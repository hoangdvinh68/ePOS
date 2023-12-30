using System.Text.Json.Serialization;
using ePOS.Domain.ItemAggregate;

namespace ePOS.Domain.UnitAggregate;

public class Unit : AuditableEntity
{
    [JsonIgnore]
    public Guid TenantId { get; set; }
    
    public string Name { get; set; } = default!;
    
    public UnitType Type { get; set; }
    
    [JsonIgnore]
    public List<Item>? Items { get; set; }
}

public enum UnitType
{
    Default = 0,
    Manual
}