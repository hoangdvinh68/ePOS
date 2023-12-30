using ePOS.Application.Contracts;
using ePOS.Shared.Mediator;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Application.Features.Unit.Queries;

public class ListUnitQuery : IAPIRequest<List<Domain.UnitAggregate.Unit>>
{
    
}

public class ListUnitQueryHandle : APIRequestHandle<ListUnitQuery, List<Domain.UnitAggregate.Unit>>
{
    private readonly ITenantContext _context;

    public ListUnitQueryHandle(ITenantContext context)
    {
        _context = context;
    }

    protected override Task<List<Domain.UnitAggregate.Unit>> HandleAsync(ListUnitQuery request, CancellationToken cancellationToken)
    {
        return _context.Units.ToListAsync(cancellationToken);
    }
}