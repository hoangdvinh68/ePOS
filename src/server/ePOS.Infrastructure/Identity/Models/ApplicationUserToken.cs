using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ePOS.Infrastructure.Identity.Models;

public class ApplicationUserToken : IdentityUserToken<Guid>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public DateTime Expires { get; set; }

    public virtual ApplicationUser User { get; set; } = default!;
}