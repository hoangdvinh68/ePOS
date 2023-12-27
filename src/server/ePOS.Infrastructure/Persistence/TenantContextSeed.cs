using Microsoft.Data.SqlClient;
using Polly;

namespace ePOS.Infrastructure.Persistence;

public static class TenantContextSeed
{
    public static async Task SeedAsync(TenantContext context, ILogger<TenantContext> logger, IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        
        var policy = Policy.Handle<SqlException>().WaitAndRetryAsync(
            retryCount: 5,
            sleepDurationProvider: _ => TimeSpan.FromSeconds(10),
            onRetry: (exception, retryCount) =>
            {
                logger.LogError("Retry {0}/{1} - Exception {2}: {3}", retryCount, 5, exception.GetType().Name, exception.Message);
            });

        await policy.ExecuteAsync(async () =>
        {
            await SeedUsersAsync(context, userManager);
            await context.SaveChangesAsync();
        });
    }

    private static async Task SeedUsersAsync(TenantContext context, UserManager<ApplicationUser> userManager)
    {
        if (!await context.Users.AnyAsync(x => x.Email.Equals("admin@epos.com")))
        {
            var userBaseEntry = await context.Users.AddAsync(new ApplicationUser()
            {
                Id = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "Owner",
                Email = "admin@epos.com",
                UserName = "admin@epos.com",
                Status = UserStatus.Active,
                CreatedAt = DateTimeOffset.Now,
            });
            
            await userManager.CreateAsync(userBaseEntry.Entity, "Admin@123");
        }
    }
}