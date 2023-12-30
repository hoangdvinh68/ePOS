using System.ComponentModel.DataAnnotations.Schema;
using ePOS.Domain.ShopAggregate;

namespace ePOS.Domain.LocationAggregate;

public class City : Entity
{
    public string Name { get; set; } = default!;
    
    public int TimeZoneUtcOffset { get; set; }
    
    public Guid CountryId { get; set; }
    [ForeignKey(nameof(CountryId))]
    public Country Country { get; set; } = default!;
    
    public List<Shop>? Shops { get; set; }
}