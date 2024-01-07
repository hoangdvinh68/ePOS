using ePOS.Application.Contracts;
using ePOS.Application.Exceptions;
using ePOS.Application.Mediator;
using ePOS.Domain.UnitAggregate;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Application.Features.Business.Commands;

public class UpdateUnitCommand : IAPIRequest<Unit>
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

public class UpdateUnitCommandHandler : APIRequestHandler<UpdateUnitCommand, Unit>
{
    private readonly ITenantContext _context;

    public UpdateUnitCommandHandler(IUserService userService, ITenantContext context) : base(userService)
    {
        _context = context;
    }

    protected override async Task<Unit> HandleAsync(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
        var unit = await _context.Units
            .FirstOrDefaultAsync(x => x.Id.Equals(request.Id) && x.TenantId.Equals(UserClaimsValue.TenantId), cancellationToken);
        if (unit is null) throw new RecordNotFound(nameof(Unit), request.Id);
        unit.Name = request.Name;
        unit.SetModificationTracking(UserClaimsValue.Id);
        await _context.SaveChangesAsync(cancellationToken);
        return unit;
    }
}