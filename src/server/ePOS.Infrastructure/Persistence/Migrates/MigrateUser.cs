using ePOS.Domain.TenantAggregate;
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
            var defaultCurrency = await context.Currencies.FirstOrDefaultAsync(x => x.IsoCode.Equals("VND"));
            if (defaultCurrency is null) throw new ApplicationException("SeedCurrencyFailed");
            var tenant = new Tenant()
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                Code = "admin001",
                CurrencyId = defaultCurrency.Id,
                TaxRate = 0
            };
            await context.Tenants.AddAsync(tenant);
            var userBaseEntry = await context.Users.AddAsync(new ApplicationUser()
            {
                Id = Guid.NewGuid(),
                TenantId = tenant.Id,
                FirstName = "Super",
                LastName = "Admin",
                Email = "admin@epos.com",
                UserName = "admin@epos.com",
                Status = UserStatus.Active,
                CreatedAt = DateTimeOffset.Now,
            });
            tenant.SetCreationTracking(userBaseEntry.Entity.Id);
            await userManager.CreateAsync(userBaseEntry.Entity, "Admin@123");
            await context.SaveChangesAsync();
        }
    }
}