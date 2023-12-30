using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ePOS.Domain.ItemAggregate;

public class ItemImage : Entity
{
    public Guid ItemId { get; set; }
    [ForeignKey(nameof(ItemId))]
    public Item Item { get; set; } = default!;
    
    [Range(0, 10)]
    public int Order { get; set; }

    public string Url { get; set; } = default!;
}