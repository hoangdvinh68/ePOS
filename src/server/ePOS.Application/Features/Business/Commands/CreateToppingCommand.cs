using ePOS.Application.Contracts;
using ePOS.Application.Exceptions;
using ePOS.Application.Mediator;
using ePOS.Domain.ShopAggregate;
using ePOS.Domain.ToppingAggregate;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Application.Features.Business.Commands;

public class CreateToppingCommand : IAPIRequest<Topping>
{
    public Guid ShopId { get; set; }
    
    public string Name { get; set; } = default!;
    
    public double Price { get; set; }
}

public class CreateToppingCommandValidator : AbstractValidator<CreateToppingCommand>
{
    public CreateToppingCommandValidator()
    {
        RuleFor(x => x.ShopId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price).NotEmpty();
    }
}

public class CreateToppingCommandHandler : APIRequestHandler<CreateToppingCommand, Topping>
{
    private readonly ITenantContext _context;
    
    public CreateToppingCommandHandler(IUserService userService, ITenantContext context) : base(userService)
    {
        _context = context;
    }

    protected override async Task<Topping> HandleAsync(CreateToppingCommand request, CancellationToken cancellationToken)
    {
        if (!await _context.Shops.AnyAsync(x => x.Id.Equals(request.ShopId), cancellationToken))
        {
            throw new RecordNotFound(nameof(Shop), request.ShopId);
        }
        var topping = new Topping()
        {
            Name = request.Name,
            Price = request.Price,
            ShopId = request.ShopId
        };
        topping.SetCreationTracking(UserClaimsValue.TenantId, UserClaimsValue.Id);
        var entryEntity = await _context.Toppings.AddAsync(topping, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entryEntity.Entity;
    }
}