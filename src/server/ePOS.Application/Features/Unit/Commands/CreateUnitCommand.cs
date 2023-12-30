using ePOS.Application.Contracts;
using ePOS.Domain.UnitAggregate;
using ePOS.Shared.Mediator;
using FluentValidation;

namespace ePOS.Application.Features.Unit.Commands;

public class CreateUnitCommand : IAPIRequest<Domain.UnitAggregate.Unit>
{
    public string Name { get; set; } = default!;
}

public class CreateUnitCommandValidator : AbstractValidator<CreateUnitCommand>
{
    public CreateUnitCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}   
    
public class CreateUnitCommandCommandHandle : APIRequestHandle<CreateUnitCommand, Domain.UnitAggregate.Unit>
{
    private readonly ITenantContext _context;

    public CreateUnitCommandCommandHandle(ITenantContext context)
    {
        _context = context;
    }

    protected override async Task<Domain.UnitAggregate.Unit> HandleAsync(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        var unit = new Domain.UnitAggregate.Unit()
        {
            Name = request.Name,
            Type = UnitType.Manual
        };
        unit.SetCreationTracking(null);
        var entryEntity = await _context.Units.AddAsync(unit, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entryEntity.Entity;
    }
}