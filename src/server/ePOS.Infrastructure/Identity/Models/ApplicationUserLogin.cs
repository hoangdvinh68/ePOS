using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ePOS.Infrastructure.Identity.Models;

[Table(nameof(ApplicationUserLogin), Schema = "Identity")]
public class ApplicationUserLogin : IdentityUserLogin<Guid>
{
    public virtual ApplicationUser User { get; set; } = default!;
}