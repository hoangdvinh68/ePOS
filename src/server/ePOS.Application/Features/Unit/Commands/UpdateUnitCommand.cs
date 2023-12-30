using ePOS.Application.Contracts;
using ePOS.Shared.Exceptions;
using ePOS.Shared.Mediator;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Application.Features.Unit.Commands;

public class UpdateUnitCommand : IAPIRequest<Domain.UnitAggregate.Unit>
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;
}

public class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
{
    public UpdateUnitCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class UpdateUnitCommandHandle : APIRequestHandle<UpdateUnitCommand, Domain.UnitAggregate.Unit>
{
    private readonly ITenantContext _context;

    public UpdateUnitCommandHandle(ITenantContext context)
    {
        _context = context;
    }

    protected override async Task<Domain.UnitAggregate.Unit> HandleAsync(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
        var unit = await _context.Units.FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken);
        if (unit is null) throw new RecordNotFound(nameof(Domain.UnitAggregate.Unit));
        unit.Name = request.Name;
        unit.SetModificationTracking(null);
        await _context.SaveChangesAsync(cancellationToken);
        return unit;
    }
}