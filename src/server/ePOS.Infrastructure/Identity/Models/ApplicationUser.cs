namespace ePOS.Infrastructure.Identity.Models;

public class ApplicationUser : IdentityUser<Guid>, IAuditableEntity
{
    public long SubId { get; set; }

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
}

public enum UserStatus
{
    Active,
    Lock,
    Deleted
}