using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ePOS.Domain;

public interface IEntity
{
    public Guid Id { get; set; }
    
    public Guid TenantId { get; set; }
    
    public long SubId { get; set; }
}

public class Entity : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public long SubId { get; set; }
}
