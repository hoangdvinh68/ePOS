using System.ComponentModel.DataAnnotations.Schema;
using ePOS.Domain.ShopAggregate;
using ePOS.Domain.ToppingAggregate;

namespace ePOS.Domain.ItemAggregate;

public class ItemTopping
{
    public Guid ItemId { get; set; }
    [ForeignKey(nameof(ItemId))]
    public Item Item { get; set; } = default!;
    
    public Guid ToppingId { get; set; }
    [ForeignKey(nameof(ToppingId))]
    public Topping Topping { get; set; } = default!;
}