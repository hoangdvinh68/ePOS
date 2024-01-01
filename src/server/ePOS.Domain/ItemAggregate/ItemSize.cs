using System.ComponentModel.DataAnnotations.Schema;

namespace ePOS.Domain.ItemAggregate;

public class ItemSize : Entity
{
    public Guid ItemId { get; set; }
    [ForeignKey(nameof(ItemId))] 
    public Item Item { get; set; } = default!;
    
    public string Size { get; set; } = default!;
    
    public double Price { get; set; }
}