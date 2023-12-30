using System.ComponentModel.DataAnnotations.Schema;

namespace ePOS.Domain.ItemAggregate;

public class ItemPropertyValue : Entity
{
    public Guid ItemPropertyId { get; set; }
    [ForeignKey(nameof(ItemPropertyId))]
    public ItemProperty ItemProperty { get; set; } = default!;

    public string Value { get; set; } = default!;
}