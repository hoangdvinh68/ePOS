namespace ePOS.Infrastructure.Identity.Models;

public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
{
    public virtual ApplicationRole Role { get; set; } = default!;
}