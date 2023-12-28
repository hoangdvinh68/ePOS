using ePOS.Domain.Common;
using ePOS.Infrastructure.Identity.Models;
using ePOS.Shared.Contracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Infrastructure.Persistence;

public class TenantContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
    ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
    ApplicationRoleClaim, ApplicationUserToken>, ITenantContext
{
    public TenantContext(DbContextOptions<TenantContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new ApplicationUser().ModelCreating(modelBuilder);
        new ApplicationRole().ModelCreating(modelBuilder);
    }

    private static void ModelCreatingSequence<T>(ModelBuilder modelBuilder) where T : class, IEntity
    {
        var sequence = $"Sequence_{typeof(T).Name}";
        modelBuilder.HasSequence<int>(sequence);
        modelBuilder.Entity<T>().Property(x => x.SubId).HasDefaultValueSql($"NEXT VALUE FOR {sequence}");
        modelBuilder.Entity<T>().HasIndex(x => x.SubId);
    }
}