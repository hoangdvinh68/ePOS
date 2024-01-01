using ePOS.Domain.UnitAggregate;

namespace ePOS.Infrastructure.Persistence.Migrates;

public static class MigrateUnit
{
    public static async Task SeedUnitAsync(TenantContext context)
    {
        foreach (var unit in Units.Where(unit => !context.Units.Any(x => x.Name.Equals(unit.Name))))
        {
            await context.AddAsync(unit);
            unit.SetCreationTracking(default, null);
        }
        await context.SaveChangesAsync();
    }

    private static readonly List<Unit> Units = new List<Unit>()
    {
        new Unit()
        {
            Id = Guid.NewGuid(),
            Name = "inch (in)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Id = Guid.NewGuid(),
            Name = "kilogram (kg)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Id = Guid.NewGuid(),
            Name = "centimeter (cm)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Id = Guid.NewGuid(),
            Name = "cup (c)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Id = Guid.NewGuid(),
            Name = "litre (l)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Id = Guid.NewGuid(),
            Name = "meter (m)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Id = Guid.NewGuid(),
            Name = "milligram (mg)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Id = Guid.NewGuid(),
            Name = "millilitre (ml)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Id = Guid.NewGuid(),
            Name = "gram (g)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Id = Guid.NewGuid(),
            Name = "pieces (pcs)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Id = Guid.NewGuid(),
            Name = "tablespoon (tbsp)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
        new Unit()
        {
            Id = Guid.NewGuid(),
            Name = "teaspoon (tsp)",
            Type = UnitType.Default,
            TenantId = Guid.Empty
        },
    };
}