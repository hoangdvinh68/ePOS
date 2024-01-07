using ePOS.Application.Contracts;
using ePOS.Application.Exceptions;
using ePOS.Application.Mediator;
using ePOS.Domain.UnitAggregate;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Application.Features.Business.Commands;

public class DeleteUnitCommand : IAPIRequest
{
    public Guid Id { get; set; }
}

public class DeleteUnitCommandValidator : AbstractValidator<DeleteUnitCommand>
{
    public DeleteUnitCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public class DeleteUnitCommandHandler : APIRequestHandler<DeleteUnitCommand>
{
    private readonly ITenantContext _context;
    
    public DeleteUnitCommandHandler(IUserService userService, ITenantContext context) : base(userService)
    {
        _context = context;
    }

    public override async Task HandleAsync(DeleteUnitCommand request, CancellationToken cancellationToken)
    {
        var record = await _context.Units
            .FirstOrDefaultAsync(x => x.Id.Equals(request.Id) && x.TenantId.Equals(UserClaimsValue.TenantId), cancellationToken);
        if (record is null) throw new RecordNotFound(nameof(Unit), request.Id);
        _context.Units.Remove(record);
        await _context.SaveChangesAsync(cancellationToken);
    }
}