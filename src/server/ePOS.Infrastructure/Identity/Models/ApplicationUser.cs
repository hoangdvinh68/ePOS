using System.ComponentModel.DataAnnotations.Schema;
using ePOS.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Infrastructure.Identity.Models;

[Table(nameof(ApplicationUser), Schema = "Identity")]
public class ApplicationUser : IdentityUser<Guid>, IAuditableEntity
{
    public long SubId { get; set; }
    
    public Guid TenantId { get; set; }
    
    public string? Description { get; set; }
    
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;
    
    public string FullName => $"{FirstName} {LastName}";
    
    public UserStatus Status { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public Guid? CreatedBy { get; set; }
    
    public DateTimeOffset? ModifiedAt { get; set; }
    
    public Guid? ModifiedBy { get; set; }

    public void SetCreate(Guid? userId)
    {
        CreatedAt = DateTimeOffset.UtcNow;
        CreatedBy = userId;
    }
    
    public void SetModify(Guid? userId)
    {
        ModifiedAt = DateTimeOffset.UtcNow;
        ModifiedBy = userId;
    }

    public virtual ICollection<ApplicationUserClaim> Claims { get; set; } = default!;
    
    public virtual ICollection<ApplicationUserLogin> Logins { get; set; } = default!;
    
    public virtual ICollection<ApplicationUserToken> Tokens { get; set; } = default!;
    
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = default!;
    
    public virtual void ModelCreating(ModelBuilder modelBuilder)
    {
        const string sequence = $"Sequence_{nameof(ApplicationUser)}";
        modelBuilder.HasSequence<int>(sequence);
        modelBuilder.Entity<ApplicationUser>()
            .Property(x => x.SubId)
            .HasDefaultValueSql($"NEXT VALUE FOR {sequence}");
        modelBuilder.Entity<ApplicationUser>().HasIndex(x => x.SubId);
        
        // Each User can have many UserClaims
        modelBuilder.Entity<ApplicationUser>()
            .HasMany(e => e.Claims)
            .WithOne(e => e.User)
            .HasForeignKey(uc => uc.UserId)
            .IsRequired();

        // Each User can have many UserLogins
        modelBuilder.Entity<ApplicationUser>()
            .HasMany(e => e.Logins)
            .WithOne(e => e.User)
            .HasForeignKey(ul => ul.UserId)
            .IsRequired();

        // Each User can have many UserTokens
        modelBuilder.Entity<ApplicationUser>()
            .HasMany(e => e.Tokens)
            .WithOne(e => e.User)
            .HasForeignKey(ut => ut.UserId)
            .IsRequired();

        // Each User can have many entries in the UserRole join table
        modelBuilder.Entity<ApplicationUser>()
            .HasMany(e => e.UserRoles)
            .WithOne(e => e.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
    }
}

public enum UserStatus
{
    Active,
    Lock,
}