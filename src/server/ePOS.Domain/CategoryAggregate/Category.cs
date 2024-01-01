using System.Text.Json.Serialization;

namespace ePOS.Domain.CategoryAggregate;

public class Category : AuditableEntity
{
    public Guid ShopId { get; set; }
    
    public string Name { get; set; } = default!;
    
    public CategoryStatus Status { get; set; }
    
    [JsonIgnore]
    public List<CategoryItem>? CategoryItems { get; set; }
}

public enum CategoryStatus
{
    Active,
    Lock
}