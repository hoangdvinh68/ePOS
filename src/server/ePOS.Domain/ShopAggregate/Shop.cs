namespace ePOS.Domain.ShopAggregate;

public class Shop : AuditableEntity
{
    public string Name { get; set; } = default!;
    
    public ShopStatus Status { get; set; }
    
    public string? Address { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public string? WifiPassword { get; set; }
    
    public string? Description { get; set; }
}

public enum ShopStatus
{
    Active,
    Lock
}