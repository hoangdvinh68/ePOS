using Microsoft.Extensions.DependencyInjection;

namespace ePOS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddIdentityServices(appSettings);
        services.AddPersistence(appSettings);
        services.AddServiceExtensions();
        return services;
    }
    
    private static IServiceCollection AddServiceExtensions(this IServiceCollection services)
    {
        services.AddScoped<ITenantContext, TenantContext>();
        services.AddTransient<IJwtTokenProvider, JwtTokenProvider>();
        services.AddTransient<IMailService, MailService>();
        return services;
    }
    
    private static IServiceCollection AddPersistence(this IServiceCollection services, AppSettings appSettings)
    {
        var connectionString = appSettings.ConnectionStrings.SQLServerConnection;
        services.AddDbContext<TenantContext>(o =>
        {
            o.UseSqlServer(connectionString);
        });
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
        return services;
    }
}