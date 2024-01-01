using ePOS.Application.Contracts;
using ePOS.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Application.Features.Business.Queries;

public class ListUnitQuery : IAPIRequest<List<Domain.UnitAggregate.Unit>>
{
    
}

public class ListUnitQueryHandler : APIRequestHandler<ListUnitQuery, List<Domain.UnitAggregate.Unit>>
{
    private readonly ITenantContext _context;

    public ListUnitQueryHandler(IUserService userService, ITenantContext context) : base(userService)
    {
        _context = context;
    }

    protected override Task<List<Domain.UnitAggregate.Unit>> HandleAsync(ListUnitQuery request, CancellationToken cancellationToken)
    {
        return _context.Units
            .Where(x => x.TenantId.Equals(UserClaimsValue.TenantId) || x.TenantId.Equals(Guid.Empty))
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}