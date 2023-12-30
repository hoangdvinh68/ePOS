using ePOS.Domain.TenantAggregate;

namespace ePOS.Domain.CurrencyAggregate;

public class Currency : Entity
{
    public string Name { get; set; } = default!;

    public string IsoCode { get; set; } = default!;

    public string Symbol { get; set; } = default!;
    
    public List<Tenant>? Tenants { get; set; }
}