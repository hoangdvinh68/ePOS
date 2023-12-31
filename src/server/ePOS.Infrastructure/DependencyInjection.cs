using System.Text;
using ePOS.Application.Contracts;
using ePOS.Infrastructure.Identity.Models;
using ePOS.Infrastructure.Persistence;
using ePOS.Infrastructure.Providers;
using ePOS.Infrastructure.Services;
using ePOS.Shared.ValueObjects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using IMailService = ePOS.Application.Contracts.IMailService;
using MailService = ePOS.Infrastructure.Services.MailService;

namespace ePOS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddIdentityServices(appSettings);
        services.AddTenantContext(appSettings);
        services.AddServiceRegistrations();
        return services;
    }
    
    private static IServiceCollection AddServiceRegistrations(this IServiceCollection services)
    {
        services.AddTransient<IJwtTokenProvider, JwtTokenProvider>();
        services.AddTransient<IMailService, MailService>();
        services.AddTransient<IUserService, UserService>();
        return services;
    }
    
    private static IServiceCollection AddTenantContext(this IServiceCollection services, AppSettings appSettings)
    {
        var connectionString = appSettings.ConnectionStrings.SQLServerConnection;
        services.AddDbContext<TenantContext>(o =>
        {
            o.UseSqlServer(connectionString);
        });
        services.AddScoped<ITenantContext, TenantContext>();
        return services;
    }
    
    private static IServiceCollection AddIdentityServices(this IServiceCollection services, AppSettings appSettings)
    {
        services
            .AddIdentity<ApplicationUser, ApplicationRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<TenantContext>()
            .AddDefaultTokenProviders();
        services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtTokenSetting.ServerSecretKey))
                };
            });
        return services;
    }
}