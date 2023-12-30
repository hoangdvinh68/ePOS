using ePOS.Domain.CategoryAggregate;
using ePOS.Domain.CurrencyAggregate;
using ePOS.Domain.ItemAggregate;
using ePOS.Domain.LocationAggregate;
using ePOS.Domain.ShopAggregate;
using ePOS.Domain.TenantAggregate;
using ePOS.Domain.ToppingAggregate;
using ePOS.Domain.UnitAggregate;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Application.Contracts;

public interface ITenantContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    DbSet<Tenant> Tenants { get; set; }
    
    DbSet<Shop> Shops { get; set; }
    
    DbSet<City> Cities { get; set; }
    
    DbSet<Country> Countries { get; set; }
    
    DbSet<Currency> Currencies { get; set; }
    
    DbSet<Unit> Units { get; set; }
    
    DbSet<Category> Categories { get; set; }
    
    DbSet<CategoryItem> CategoryItems { get; set; }
    
    DbSet<Item> Items { get; set; }

    DbSet<ItemImage> ItemImages { get; set; }
    
    DbSet<ItemProperty> ItemProperties { get; set; }

    DbSet<ItemPropertyValue> ItemPropertyOptions { get; set; }
    
    DbSet<ItemSize> ItemSizes { get; set; }
    
    DbSet<ItemTopping> ItemToppings { get; set; }
    
    DbSet<Topping> Toppings { get; set; }
}