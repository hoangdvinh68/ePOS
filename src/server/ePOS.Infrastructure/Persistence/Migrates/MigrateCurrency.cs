using ePOS.Domain.CurrencyAggregate;

namespace ePOS.Infrastructure.Persistence.Migrates;

public static class MigrateCurrency
{
    public static async Task SeedCurrencyAsync(TenantContext context)
    {
        foreach (var currency in Currencies.Where(currency => !context.Currencies.Any(x => x.IsoCode.Equals(currency.IsoCode))))
        {
            await context.Currencies.AddAsync(currency);
        }
        await context.SaveChangesAsync();
    }

    private static readonly List<Currency> Currencies = new List<Currency>()
    {
        new Currency()
        {
            Id = Guid.NewGuid(),
            Name = "United States dollar",
            IsoCode = "USD",
            Symbol = "$"
        },
        new Currency()
        {
            Id = Guid.NewGuid(),
            Name = "Vietnamese dong",
            IsoCode = "VND",
            Symbol = "\u20ab",
        },
    };
}