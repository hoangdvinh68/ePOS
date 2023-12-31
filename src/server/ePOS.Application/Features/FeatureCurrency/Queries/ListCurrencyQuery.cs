using ePOS.Application.Contracts;
using ePOS.Application.Mediator;
using ePOS.Domain.CurrencyAggregate;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Application.Features.FeatureCurrency.Queries;

public class ListCurrencyQuery : IAPIRequest<List<Currency>>
{
    
}

public class ListCurrencyQueryHandle : APIRequestHandle<ListCurrencyQuery, List<Currency>>
{
    private readonly ITenantContext _context;
    
    public ListCurrencyQueryHandle(IUserService userService, ITenantContext context) : base(userService)
    {
        _context = context;
    }

    protected override Task<List<Currency>> HandleAsync(ListCurrencyQuery request, CancellationToken cancellationToken)
    {
        return _context.Currencies
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }
}