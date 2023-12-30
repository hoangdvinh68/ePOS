using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ePOS.Infrastructure.Identity.Models;

[Table(nameof(ApplicationUserClaim), Schema = "Identity")]
public class ApplicationUserClaim : IdentityUserClaim<Guid>
{
    public virtual ApplicationUser User { get; set; } = default!;
}