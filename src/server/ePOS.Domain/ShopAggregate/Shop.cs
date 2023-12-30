using System.ComponentModel.DataAnnotations.Schema;
using ePOS.Domain.LocationAggregate;

namespace ePOS.Domain.ShopAggregate;

public class Shop : AuditableEntity
{
    public Guid TenantId { get; set; }
    
    public string Name { get; set; } = default!;
    
    public bool IsDefault { get; set; }
    
    public string? Address { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public string? WifiPassword { get; set; }
    
    public string? Description { get; set; }

    public Guid? CityId { get; set; }
    [ForeignKey(nameof(CityId))]
    public City? City { get; set; }
    
    public Guid? CountryId { get; set; }
    [ForeignKey(nameof(CountryId))]
    public Country? Country { get; set; }
}