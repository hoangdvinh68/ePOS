using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ePOS.Domain;

public interface IEntity
{
    public Guid Id { get; set; }
    
    public long SubId { get; set; }
}

public class Entity : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonProperty(Order = -2)]
    public Guid Id { get; set; }
    
    [JsonProperty(Order = -1)]
    public long SubId { get; set; }
}
