namespace ePOS.Infrastructure.Persistence;

public class TenantContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
    ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
    ApplicationRoleClaim, ApplicationUserToken>, ITenantContext
{
    public TenantContext(DbContextOptions<TenantContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicationRoleConfiguration());
        ModelCreating<ApplicationUser>(modelBuilder);
        ModelCreating<ApplicationRole>(modelBuilder);
    }

    private static void ModelCreating<T>(ModelBuilder modelBuilder) where T : class, IEntity
    {
        var sequence = $"Sequence_{typeof(T).Name}";
        modelBuilder.HasSequence<int>(sequence);
        modelBuilder.Entity<T>().Property(x => x.SubId).HasDefaultValueSql($"NEXT VALUE FOR {sequence}");
        modelBuilder.Entity<T>().HasIndex(x => x.SubId);
    }
}