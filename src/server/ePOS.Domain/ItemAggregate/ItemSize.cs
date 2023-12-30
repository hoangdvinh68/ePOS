using System.ComponentModel.DataAnnotations;

namespace ePOS.Domain.ItemAggregate;

public class ItemSize : Entity
{
    public string Size { get; set; } = default!;
    
    [Range(0, double.MaxValue)]
    public double Price { get; set; }
}