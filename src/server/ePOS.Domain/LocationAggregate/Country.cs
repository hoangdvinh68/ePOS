using ePOS.Domain.ShopAggregate;

namespace ePOS.Domain.LocationAggregate;

public class Country : Entity
{
    public string Name { get; set; } = default!;

    public List<City> Cities { get; set; } = default!;
    
    public List<Shop>? Shops { get; set; }
}