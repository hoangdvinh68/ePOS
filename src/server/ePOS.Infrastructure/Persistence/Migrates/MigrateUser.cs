using ePOS.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Infrastructure.Persistence.Migrates;

public static class MigrateUser
{
    public static async Task SeedUsersAsync(TenantContext context, UserManager<ApplicationUser> userManager)
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