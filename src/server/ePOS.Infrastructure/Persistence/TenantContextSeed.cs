using ePOS.Infrastructure.Identity.Models;
using ePOS.Infrastructure.Persistence.Migrates;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

namespace ePOS.Infrastructure.Persistence;

public static class TenantContextSeed
{
    public static async Task SeedAsync(TenantContext context, ILogger<TenantContext> logger, IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        var policy = Policy.Handle<SqlException>().WaitAndRetryAsync(
            retryCount: 5,
            sleepDurationProvider: _ => TimeSpan.FromSeconds(10),
            onRetry: (exception, retryCount) =>
            {
                logger.LogError("Retry {0}/{1} - Exception {2}: {3}", retryCount, 5, exception.GetType().Name, exception.Message);
            });

        await policy.ExecuteAsync(async () =>
        {
            await MigrateUser.SeedUsersAsync(context, userManager);
            await context.SaveChangesAsync();
        });
    }
}