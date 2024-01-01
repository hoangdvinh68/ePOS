using System.ComponentModel.DataAnnotations.Schema;
using ePOS.Domain.CategoryAggregate;
using ePOS.Domain.UnitAggregate;

namespace ePOS.Domain.ItemAggregate;

public class Item : AuditableEntity
{
    public Guid ShopId { get; set; }
    
    public string Sku { get; set; } = default!;
    
    public string Name { get; set; } = default!;
    
    public double? Price { get; set; }
    
    public ItemStatus Status { get; set; }
    
    public int? TaxRate { get; set; }
    
    public bool? IsTaxIncludePrice { get; set; }
    
    public Guid? UnitId { get; set; }
    [ForeignKey(nameof(UnitId))]
    public Unit? Unit { get; set; }
    
    public int? MaxTopping { get; set; }
    
    public List<ItemImage>? Images { get; set; }
    
    public List<ItemSize>? Sizes { get; set; }
    
    public List<ItemTopping>? ItemToppings { get; set; }
    
    public List<CategoryItem>? CategoryItems { get; set; }
    
    public List<ItemProperty>? ItemProperties { get; set; }
}

public enum ItemStatus
{
    Active,
    Lock
}