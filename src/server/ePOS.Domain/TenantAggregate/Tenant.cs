using System.ComponentModel.DataAnnotations.Schema;
using ePOS.Domain.CurrencyAggregate;

namespace ePOS.Domain.TenantAggregate;

public sealed class Tenant : AuditableEntity
{
    public string Name { get; set; }

    public string Code { get; set; }
    
    public Guid CurrencyId { get; set; }
    [ForeignKey(nameof(CurrencyId))]
    public Currency Currency { get; set; } = default!;
    
    public string? CompanyName { get; set; }
    
    public string? CompanyAddress { get; set; }
    
    public bool IsTaxAppliedAllItem { get; set; }

    public int? TaxRate { get; set; }
    
    public bool? IsItemPriceIncludeTax { get; set; }

    public Tenant(string name, string code, Guid currencyId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Code = code;
        CurrencyId = currencyId;
        IsTaxAppliedAllItem = false;
    }
}