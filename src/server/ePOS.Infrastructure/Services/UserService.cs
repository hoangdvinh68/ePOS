using System.IdentityModel.Tokens.Jwt;
using ePOS.Application.Constants;
using ePOS.Application.Contracts;
using ePOS.Application.Exceptions;
using ePOS.Application.Extensions;
using ePOS.Application.Features.User.Commands;
using ePOS.Application.Features.User.Queries;
using ePOS.Application.Features.User.Responses;
using ePOS.Application.ValueObjects;
using ePOS.Domain.ShopAggregate;
using ePOS.Domain.TenantAggregate;
using ePOS.Infrastructure.Identity.Models;
using ePOS.Infrastructure.Persistence;
using ePOS.Infrastructure.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly TenantContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly JwtTokenSetting _jwtTokenSetting;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(TenantContext context, UserManager<ApplicationUser> userManager, 
        IJwtTokenProvider jwtTokenProvider, AppSettings appSettings, 
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _userManager = userManager;
        _jwtTokenProvider = jwtTokenProvider;
        _httpContextAccessor = httpContextAccessor;
        _jwtTokenSetting = appSettings.JwtTokenSetting;
    }

    public async Task<SignInResponse> SignInAsync(SignInCommand command, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email.Equals(command.Email), cancellationToken);
        if (user is null) throw new BadRequestException("EmailNotFound");
        var checkedPassword = await _userManager.CheckPasswordAsync(user, command.Password);
        if (!checkedPassword) throw new BadRequestException("IncorrectPassword");
        var accessToken = _jwtTokenProvider.GenerateJwtToken(user);
        var userToken = await _context.UserTokens
            .FirstOrDefaultAsync(x => x.UserId.Equals(user.Id) && x.LoginProvider.Equals(LoginProvider.Jwt), cancellationToken);
        if (userToken is not null) _context.UserTokens.Remove(userToken);
        var entryEntity = await _context.UserTokens.AddAsync(new ApplicationUserToken()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            LoginProvider = LoginProvider.Jwt,
            Name = user.Email,
            Value = accessToken,
            Expires = DateTime.Now.AddMinutes(_jwtTokenSetting.RefreshTokenExpirationMinutes)
        }, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return new SignInResponse()
        {
            AccessToken = accessToken,
            RefreshToken = entryEntity.Entity.Id.ToString("N"),
            Expires = entryEntity.Entity.Expires
        };
    }

    public async Task<SignUpResponse> SignUpAsync(SignUpCommand command, CancellationToken cancellationToken)
    {
        if(await _context.Users.AnyAsync(x => x.Email.Equals(command),cancellationToken))
        {
            throw new BadRequestException("EmailExisted");
        }
        var defaultCurrency = await _context.Currencies.FirstOrDefaultAsync(x => x.IsoCode.Equals("VND"), cancellationToken);
        if (defaultCurrency is null) throw new ApplicationException();
        var tenant = new Tenant(
            command.TenantName,
            $"{command.TenantName}{_context.Tenants.Max(x => x.SubId)}",
            defaultCurrency.Id);
        await _context.Tenants.AddAsync(tenant, cancellationToken);
        var user = new ApplicationUser()
        {
            Id = Guid.NewGuid(),
            TenantId = tenant.Id,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Status = UserStatus.Active,
            Email = command.Email,
            UserName = command.Email
        };
        var result = await _userManager.CreateAsync(user, command.Password);
        if (!result.Succeeded) throw new BadRequestException(string.Join(";", result.Errors.Select(x => x.Code)));
        tenant.SetCreationTracking(default, user.Id);
        var shop = new Shop()
        {
            Id = Guid.NewGuid(),
            TenantId = tenant.Id,
            Name = tenant.Name,
            Status = ShopStatus.Active
        };
        shop.SetCreationTracking(tenant.Id, user.Id);
        await _context.Shops.AddAsync(shop, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return new SignUpResponse()
        {
            Id = Guid.NewGuid(),
            FullName = user.FullName,
            Email = user.Email
        };
    }

    public UserClaimsValue GetUserClaimsValue()
    {
        var authorizationValue = _httpContextAccessor.HttpContext?.Request.Headers
            .FirstOrDefault(x => x.Key.Equals("Authorization")).Value;
        var accessToken = authorizationValue?.ToString().Split(" ").LastOrDefault();
        if (string.IsNullOrEmpty(accessToken)) return new UserClaimsValue();
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var decodedToken = jwtSecurityTokenHandler.ReadJwtToken(accessToken);
        return new UserClaimsValue()
        {
            Id = decodedToken.Claims.FirstOrDefault(x => x.Type.Equals("id"))?.Value.ToGuid(),
            TenantId = decodedToken.Claims.FirstOrDefault(x => x.Type.Equals("tenantId"))?.Value.ToGuid() ?? default,
            FullName = decodedToken.Claims.FirstOrDefault(x => x.Type.Equals("fullName"))?.Value,
            Email = decodedToken.Claims.FirstOrDefault(x => x.Type.Equals("email"))?.Value,
        };
    }

    public async Task<GetProfileResponse> GetProfileAsync(GetProfileQuery query, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(query.Email), cancellationToken);
        if (user is null) throw new BadRequestException("EmailNotFound");
        return new GetProfileResponse()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }
}