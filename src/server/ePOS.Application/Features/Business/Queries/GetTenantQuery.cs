using ePOS.Application.Contracts;
using ePOS.Application.Exceptions;
using ePOS.Application.Mediator;
using ePOS.Domain.TenantAggregate;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Application.Features.Business.Queries;

public class GetTenantQuery : IAPIRequest<Tenant>
{
    public Guid TenantId { get; set; }
}

public class GetTenantQueryValidator : AbstractValidator<GetTenantQuery>
{
    public GetTenantQueryValidator()
    {
        RuleFor(x => x.TenantId).NotEmpty();
    }
}


public class GetTenantQueryHandler : APIRequestHandler<GetTenantQuery, Tenant>
{
    private readonly ITenantContext _context;
    
    public GetTenantQueryHandler(IUserService userService, ITenantContext context) : base(userService)
    {
        _context = context;
    }

    protected override async Task<Tenant> HandleAsync(GetTenantQuery request, CancellationToken cancellationToken)
    {
        var tenant = await _context.Tenants
            .FirstOrDefaultAsync(x => x.Id.Equals(request.TenantId), cancellationToken);
        if (tenant is null) throw new RecordNotFound(nameof(Tenant), request.TenantId);
        return tenant;
    }
}