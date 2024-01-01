namespace ePOS.Domain.CurrencyAggregate;

public class Currency : Entity
{
    public string Name { get; set; } = default!;

    public string IsoCode { get; set; } = default!;

    public string Symbol { get; set; } = default!;
}