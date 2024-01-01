using ePOS.Application.Contracts;
using ePOS.Domain;
using ePOS.Domain.CategoryAggregate;
using ePOS.Domain.CurrencyAggregate;
using ePOS.Domain.ItemAggregate;
using ePOS.Domain.ShopAggregate;
using ePOS.Domain.TenantAggregate;
using ePOS.Domain.ToppingAggregate;
using ePOS.Domain.UnitAggregate;
using ePOS.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ePOS.Infrastructure.Persistence;

public class TenantContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
    ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
    ApplicationRoleClaim, ApplicationUserToken>, ITenantContext
{
    public TenantContext(DbContextOptions<TenantContext> options) : base(options) { }
    
    public DbSet<Tenant> Tenants { get; set; }
    
    public DbSet<Currency> Currencies { get; set; }

    public DbSet<Unit> Units { get; set; }
    
    public DbSet<Shop> Shops { get; set; }
    
    public DbSet<Item> Items { get; set; }

    public DbSet<ItemImage> ItemImages { get; set; }
    
    public DbSet<ItemProperty> ItemProperties { get; set; }

    public DbSet<ItemPropertyValue> ItemPropertyValues { get; set; }
    
    public DbSet<ItemSize> ItemSizes { get; set; }
    
    public DbSet<ItemTopping> ItemToppings { get; set; }
    
    public DbSet<Topping> Toppings { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<CategoryItem> CategoryItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new ApplicationUser().ModelCreating(modelBuilder);
        new ApplicationRole().ModelCreating(modelBuilder);
        ModelCreating<Tenant>(modelBuilder);
        ModelCreating<Shop>(modelBuilder);
        ModelCreating<Currency>(modelBuilder);
        ModelCreating<Unit>(modelBuilder);
        ModelCreating<Category>(modelBuilder);
        modelBuilder.Entity<CategoryItem>()
            .HasKey(x => new { x.CategoryId, x.ItemId });
        ModelCreating<Item>(modelBuilder, entityBuilder =>
        {
            entityBuilder.HasIndex(x => x.Sku);
        });
        ModelCreating<ItemImage>(modelBuilder);
        ModelCreating<ItemProperty>(modelBuilder);
        ModelCreating<ItemPropertyValue>(modelBuilder);
        ModelCreating<ItemSize>(modelBuilder);
        modelBuilder.Entity<ItemTopping>()
            .HasKey(x => new { x.ItemId, x.ToppingId });
        ModelCreating<Topping>(modelBuilder);
    }
    
    private static void ModelCreating<T>(ModelBuilder modelBuilder) where T : class, IEntity
    {
        var sequence = $"Sequence_{typeof(T).Name}";
        modelBuilder.HasSequence<int>(sequence);
        modelBuilder.Entity<T>().Property(x => x.SubId).HasDefaultValueSql($"NEXT VALUE FOR {sequence}");
        modelBuilder.Entity<T>().HasIndex(x => x.SubId);
    }

    private static void ModelCreating<T>(ModelBuilder modelBuilder, Action<EntityTypeBuilder<T>> callBack) where T : class, IEntity
    {
        ModelCreating<T>(modelBuilder);
        callBack.Invoke(modelBuilder.Entity<T>());
    }
}