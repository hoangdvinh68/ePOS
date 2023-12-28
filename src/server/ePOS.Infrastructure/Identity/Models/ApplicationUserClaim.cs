using Microsoft.AspNetCore.Identity;

namespace ePOS.Infrastructure.Identity.Models;

public class ApplicationUserClaim : IdentityUserClaim<Guid>
{
    public virtual ApplicationUser User { get; set; } = default!;
}