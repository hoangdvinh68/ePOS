using ePOS.Domain.UnitAggregate;

namespace ePOS.Infrastructure.Persistence.Migrates;

public static class MigrateUnit
{
    public static async Task SeedUnitAsync(TenantContext context)
    {
        foreach (var unit in Units.Where(unit => !context.Units.Any(x => x.Name.Equals(unit.Name))))
        {
            await context.AddAsync(unit);
            unit.SetCreationTracking(null);
        }
        await context.SaveChangesAsync();
    }

    private static List<Unit> Units = new List<Unit>()
    {
        new Unit()
        {
            Name = "inch (in)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Name = "kilogram (kg)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Name = "centimeter (cm)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Name = "cup (c)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Name = "litre (l)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Name = "meter (m)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Name = "milligram (mg)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Name = "millilitre (ml)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Name = "gram (g)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Name = "pieces (pcs)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Name = "tablespoon (tbsp)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Name = "teaspoon (tsp)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
    };
}