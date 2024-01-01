using ePOS.Application.Contracts;
using ePOS.Application.Exceptions;
using ePOS.Application.Mediator;
using ePOS.Domain.ItemAggregate;
using ePOS.Domain.ShopAggregate;
using ePOS.Domain.ToppingAggregate;
using ePOS.Domain.UnitAggregate;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Application.Features.Business.Commands;

public class CreateItemCommand : IAPIRequest<Item>
{
    public Guid ShopId { get; set; }
    
    public string Name { get; set; } = default!;
    
    public Guid? UnitId { get; set; }

    public string Sku { get; set; } = default!;
    
    public double? Price { get; set; }
    
    public Dictionary<string, double>? SizePrices { get; set; }
    
    public int? TaxRate { get; set; }
    
    public bool? IsTaxIncludePrice { get; set; }
    
    public Dictionary<string, string[]>? ItemProperties { get; set; }
    
    public List<Guid>? ToppingIds { get; set; }
    
    public string[]? ImageUrls { get; set; }
}

public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemCommandValidator()
    {
        RuleFor(x => x.ShopId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price).NotEmpty();
    }
}

public class CreateItemCommandHandler : APIRequestHandler<CreateItemCommand, Item>
{
    private readonly ITenantContext _context;
    
    public CreateItemCommandHandler(IUserService userService, ITenantContext context) : base(userService)
    {
        _context = context;
    }

    protected override async Task<Item> HandleAsync(CreateItemCommand request, CancellationToken cancellationToken)
    {
        if (!await _context.Shops.AnyAsync(x => x.Id.Equals(request.ShopId), cancellationToken: cancellationToken))
        {
            throw new RecordNotFound(nameof(Shop), request.ShopId);
        }
        

        if (!await _context.Units.AnyAsync(x => x.Id.Equals(request.UnitId), cancellationToken))
        {
            throw new RecordNotFound(nameof(Unit), request.UnitId);
        }

        var item = new Item()
        {
            Id = Guid.NewGuid(),
            ShopId = request.ShopId,
            Name = request.Name,
            Sku = request.Sku,
            UnitId = request.UnitId,
            Price = request.Price,
            TaxRate = request.TaxRate,
            IsTaxIncludePrice = request.IsTaxIncludePrice,
            Status = ItemStatus.Active
        };
        item.SetCreationTracking(UserClaimsValue.TenantId, UserClaimsValue.Id);
        await _context.Items.AddAsync(item, cancellationToken);

        if (request.SizePrices is not null)
        {
            await _context.ItemSizes.AddRangeAsync(request.SizePrices.Select(x => new ItemSize()
            {
                Id = Guid.NewGuid(),
                ItemId = item.Id,
                Size = x.Key,
                Price = x.Value,
                TenantId = UserClaimsValue.TenantId,
            }), cancellationToken);
        }

        if (request.ItemProperties is not null)
        {
            await _context.ItemProperties.AddRangeAsync(request.ItemProperties
                .Select(x => new ItemProperty()
                {
                    Id = Guid.NewGuid(),
                    ItemId = item.Id,
                    TenantId = UserClaimsValue.TenantId,
                    Name = x.Key,
                    ItemPropertyValues = x.Value.Select(y => new ItemPropertyValue()
                    {
                        Id = Guid.NewGuid(),
                        TenantId = UserClaimsValue.TenantId,
                        ItemPropertyId = item.Id,
                        Value = y
                    }).ToList()
                }), cancellationToken);
        }

        if (request.ToppingIds is not null)
        {
            foreach (var toppingId in request.ToppingIds)
            {
                if (!await _context.Toppings.AnyAsync(x => x.Id.Equals(toppingId), cancellationToken))
                {
                    throw new RecordNotFound(nameof(Topping), toppingId);
                }
            }
            await _context.ItemToppings.AddRangeAsync(request.ToppingIds.Select(x => new ItemTopping()
            {
                ItemId = item.Id,
                ToppingId = x
            }), cancellationToken);
        }

        if (request.ImageUrls is not null)
        {
            await _context.ItemImages.AddRangeAsync(request.ImageUrls.Select((x, idx) => new ItemImage()
            {
                Id = Guid.NewGuid(),
                TenantId = UserClaimsValue.TenantId,
                ItemId = item.Id,
                Url = x,
                Order = idx
            }), cancellationToken);
        }
        
        await _context.SaveChangesAsync(cancellationToken);
        return item;
    }
}