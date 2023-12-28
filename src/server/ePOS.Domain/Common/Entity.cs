using System.ComponentModel.DataAnnotations;

namespace ePOS.Domain.Common;

public interface IEntity
{
    public Guid Id { get; set; }
    
    public long SubId { get; set; }
}

public class Entity : IEntity
{
    [Key]
    public Guid Id { get; set; }
    
    public long SubId { get; set; }
}

