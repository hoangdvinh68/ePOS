using System.ComponentModel.DataAnnotations.Schema;

namespace ePOS.Domain.ItemAggregate;

public class ItemProperty : Entity
{
    public string Name { get; set; } = default!;
    
    public Guid ItemId { get; set; }
    [ForeignKey(nameof(ItemId))]
    public Item Item { get; set; } = default!;

    public List<ItemPropertyValue> ItemPropertyValues { get; set; } = new();
}