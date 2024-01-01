using ePOS.Domain.ItemAggregate;

namespace ePOS.Domain.ToppingAggregate;

public class Topping : AuditableEntity
{
    public Guid ShopId { get; set; }
    
    public string Name { get; set; } = default!;
    
    public ToppingStatus Status { get; set; }

    public double Price { get; set; } = default!;
    
    public List<ItemTopping>? ItemToppings { get; set; }
}

public enum ToppingStatus
{
    Active,
    Lock
}