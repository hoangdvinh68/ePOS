using ePOS.Domain.ShopAggregate;
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
            var tenant = new Tenant("Admin", "admin001", defaultCurrency.Id);
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
            tenant.SetCreationTracking(default, userBaseEntry.Entity.Id);
            var shop = new Shop()
            {
                Id = Guid.NewGuid(),
                Name = tenant.Name,
                Status = ShopStatus.Active,
            };
            shop.SetCreationTracking(tenant.Id, userBaseEntry.Entity.Id);
            await context.Shops.AddAsync(shop);
            await userManager.CreateAsync(userBaseEntry.Entity, "Admin@123");
            await context.SaveChangesAsync();
        }
    }
}