using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ePOS.Infrastructure.Identity.Models;

[Table(nameof(ApplicationRoleClaim), Schema = "Identity")]
public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
{
    public virtual ApplicationRole Role { get; set; } = default!;
}