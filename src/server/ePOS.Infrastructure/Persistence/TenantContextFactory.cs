using ePOS.Application.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ePOS.Infrastructure.Persistence;

public class TenantContextFactory : IDesignTimeDbContextFactory<TenantContext>
{
    public TenantContext CreateDbContext(string[] args)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var appSettings = new AppSettings();
        
        new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile(Path.Combine(nameof(AppSettings), $"appsettings.{environmentName}.json"))
            .Build()
            .Bind(appSettings);
        
        var optionsBuilder = new DbContextOptionsBuilder<TenantContext>();
        optionsBuilder.UseSqlServer(appSettings.ConnectionStrings.SQLServerConnection);
        return new TenantContext(optionsBuilder.Options);
    }
}