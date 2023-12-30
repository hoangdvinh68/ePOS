using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ePOS.Infrastructure.Identity.Models;

[Table(nameof(ApplicationUserRole), Schema = "Identity")]
public class ApplicationUserRole : IdentityUserRole<Guid>
{
    public virtual ApplicationUser User { get; set; } = default!;
    
    public virtual ApplicationRole Role { get; set; } = default!;
}