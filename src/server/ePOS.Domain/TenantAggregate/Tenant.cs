using System.ComponentModel.DataAnnotations.Schema;
using ePOS.Domain.CurrencyAggregate;

namespace ePOS.Domain.TenantAggregate;

public sealed class Tenant : AuditableEntity
{
    public string Name { get; set; } = default!;
    
    public string Code { get; set; } = default!;
    
    public string? CompanyName { get; set; }
    
    public int TaxRate { get; set; }
    
    public Guid CurrencyId { get; set; }
    [ForeignKey(nameof(CurrencyId))]
    public Currency Currency { get; set; } = default!;
}