using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ePOS.Infrastructure.Identity.Models;

[Table(nameof(ApplicationUserToken), Schema = "Identity")]
public class ApplicationUserToken : IdentityUserToken<Guid>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public DateTime Expires { get; set; }

    public virtual ApplicationUser User { get; set; } = default!;
}