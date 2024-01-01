using ePOS.Application.Contracts;
using ePOS.Application.Mediator;
using ePOS.Domain.ShopAggregate;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Application.Features.Business.Queries;

public class ListShopQuery : IAPIRequest<List<Shop>>
{
    
}

public class ListShopQueryHandler : APIRequestHandler<ListShopQuery, List<Shop>>
{
    private readonly ITenantContext _context;
    
    public ListShopQueryHandler(IUserService userService, ITenantContext context) : base(userService)
    {
        _context = context;
    }

    protected override async Task<List<Shop>> HandleAsync(ListShopQuery request, CancellationToken cancellationToken)
    {
        return await _context.Shops
            .Where(x => x.TenantId.Equals(UserClaimsValue.TenantId))
            .ToListAsync(cancellationToken);
    }
}