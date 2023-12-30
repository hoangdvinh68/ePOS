using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ePOS.Domain.CategoryAggregate;
using ePOS.Domain.UnitAggregate;

namespace ePOS.Domain.ItemAggregate;

public class Item : AuditableEntity
{
    public Guid ShopId { get; set; }
    
    public string Name { get; set; } = default!;
    
    public string Sku { get; set; } = default!;
    
    [Range(0, 100)]
    public int? TaxRate { get; set; }
    
    public bool? IsTaxInclude { get; set; }
    
    public Guid UnitId { get; set; }
    [ForeignKey(nameof(UnitId))]
    public Unit Unit { get; set; } = default!;
    
    public List<ItemImage>? Images { get; set; }
    
    public List<ItemSize>? Sizes { get; set; }
    
    public List<ItemTopping>? ItemToppings { get; set; }
    
    public List<CategoryItem>? CategoryItems { get; set; }
    
    public List<ItemProperty>? ItemProperties { get; set; }
}