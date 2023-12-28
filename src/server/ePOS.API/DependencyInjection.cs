using ePOS.Shared.ValueObjects;
using Microsoft.OpenApi.Models;

namespace ePOS.API;

public static class DependencyInjection
{
    public static IServiceCollection AddAPIServices(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddHealthChecks();
        services.AddCustomSwagger();
        services.AddHttpContextAccessor();
        return services;
    }
    
    private static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "ePOS API",
                Version = "v1"
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { 
                    new OpenApiSecurityScheme 
                    { 
                        Reference = new OpenApiReference 
                        { 
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer" 
                        } 
                    },
                    Array.Empty<string>()
                } 
            });
        });
        return services;
    }
}