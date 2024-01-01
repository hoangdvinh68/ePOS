using ePOS.Application.Contracts;
using ePOS.Application.Exceptions;
using ePOS.Application.Mediator;
using ePOS.Domain.CategoryAggregate;
using ePOS.Domain.ItemAggregate;
using ePOS.Domain.ShopAggregate;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ePOS.Application.Features.Business.Commands;

public class CreateCategoryCommand : IAPIRequest<Category>
{
    public Guid ShopId { get; set; }
    public string Name { get; set; } = default!;
    
    public List<Guid>? ItemIds { get; set; }
}

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.ShopId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class CreateCategoryCommandCommandHandler : APIRequestHandler<CreateCategoryCommand, Category>
{
    private readonly ITenantContext _context;

    public CreateCategoryCommandCommandHandler(IUserService userService, ITenantContext context) : base(userService)
    {
        _context = context;
    }

    protected override async Task<Category> HandleAsync(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (!await _context.Shops.AnyAsync(x => x.Id.Equals(request.ShopId), cancellationToken))
        {
            throw new RecordNotFound(nameof(Shop), request.ShopId);
        }
        var category = new Category()
        {
            Name = request.Name,
            ShopId = request.ShopId,
            Status = CategoryStatus.Active
        };
        var entryEntity = await _context.Categories.AddAsync(category, cancellationToken);
        if (request.ItemIds is not null)
        {
            var categoryItems = new List<CategoryItem>();
            foreach (var itemId in request.ItemIds)
            {
                var itemExisted = await _context.Items.FirstOrDefaultAsync(x => x.Id.Equals(itemId), cancellationToken);
                if (itemExisted is null) throw new RecordNotFound(nameof(Item), itemId);
                var categoryItem = new CategoryItem()
                {
                    CategoryId = category.Id,
                    ItemId = itemId
                };
                categoryItems.Add(categoryItem);
            }
            await _context.CategoryItems.AddRangeAsync(categoryItems, cancellationToken);
        }
        category.SetCreationTracking(UserClaimsValue.TenantId, UserClaimsValue.Id);
        await _context.SaveChangesAsync(cancellationToken);
        return entryEntity.Entity;
    }
}