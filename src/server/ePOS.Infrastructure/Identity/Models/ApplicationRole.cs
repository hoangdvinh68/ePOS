namespace ePOS.Infrastructure.Identity.Models;

public class ApplicationRole : IdentityRole<Guid>, IEntity
{
    public long SubId { get; set; }
    
    
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = default!;

    public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; } = default!;
}