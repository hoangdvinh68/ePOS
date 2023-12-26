using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ePOS.Infrastructure.Persistence;

public class TenantContextFactory : IDesignTimeDbContextFactory<TenantContext>
{
    public TenantContext CreateDbContext(string[] args)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        
        Console.WriteLine(Environment.CurrentDirectory);
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile(Path.Combine(nameof(AppSettings), $"appsettings.{environmentName}.json"))
            .Build();
        
        var optionsBuilder = new DbContextOptionsBuilder<TenantContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("SQLServerConnection"));
        return new TenantContext(optionsBuilder.Options);
    }
}