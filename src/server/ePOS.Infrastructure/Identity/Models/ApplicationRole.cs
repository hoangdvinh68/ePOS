using ePOS.Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Infrastructure.Identity.Models;

public class ApplicationRole : IdentityRole<Guid>, IEntity
{
    public long SubId { get; set; }
    
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = default!;

    public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; } = default!;
    
    public virtual void ModelCreating(ModelBuilder modelBuilder)
    {
        const string sequence = $"Sequence_{nameof(ApplicationRole)}";
        modelBuilder.HasSequence<int>(sequence);
        modelBuilder.Entity<ApplicationRole>()
            .Property(x => x.SubId)
            .HasDefaultValueSql($"NEXT VALUE FOR {sequence}");
        modelBuilder.Entity<ApplicationRole>().HasIndex(x => x.SubId);
        
        // Each Role can have many entries in the UserRole join table
        modelBuilder.Entity<ApplicationRole>()
            .HasMany(e => e.UserRoles)
            .WithOne(e => e.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        // Each Role can have many associated RoleClaims 
        modelBuilder.Entity<ApplicationRole>()
            .HasMany(e => e.RoleClaims)
            .WithOne(e => e.Role)
            .HasForeignKey(rc => rc.RoleId)
            .IsRequired();
    }
}