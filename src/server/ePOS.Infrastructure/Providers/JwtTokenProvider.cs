using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ePOS.Infrastructure.Identity.Models;
using ePOS.Shared.ValueObjects;
using Microsoft.IdentityModel.Tokens;

namespace ePOS.Infrastructure.Providers;

public interface IJwtTokenProvider
{
    string GenerateJwtToken(ApplicationUser user);
    string GenerateRefreshToken();
}

public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly JwtTokenSetting _jwtTokenSettings;

    public JwtTokenProvider(AppSettings appSettings)
    {
        _jwtTokenSettings = appSettings.JwtTokenSetting;
    }

    public string GenerateJwtToken(ApplicationUser user) 
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.ServerSecretKey)),
            SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>()
        {
            new Claim("id", user.Id.ToString()),  
            new Claim("tenantId", user.TenantId.ToString()),  
            new Claim("fullName", $"{user.FullName}"),
            new Claim("email", user.Email)
        };
        var expires = DateTime.Now.AddMinutes(_jwtTokenSettings.AccessTokenExpirationMinutes);
        var jwtToken = new JwtSecurityToken(
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials); 
        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return token;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}