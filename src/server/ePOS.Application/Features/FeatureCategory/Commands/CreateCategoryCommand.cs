using ePOS.Application.Contracts;
using ePOS.Application.Mediator;
using ePOS.Domain.CategoryAggregate;
using FluentValidation;

namespace ePOS.Application.Features.FeatureCategory.Commands;

public class CreateCategoryCommand : IAPIRequest<Category>
{
    public Guid ShopId { get; set; }
    public string Name { get; set; } = default!;
}

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.ShopId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class CreateCategoryCommandCommandHandle : APIRequestHandle<CreateCategoryCommand, Category>
{
    private readonly ITenantContext _context;

    public CreateCategoryCommandCommandHandle(IUserService userService, ITenantContext context) : base(userService)
    {
        _context = context;
    }

    protected override async Task<Category> HandleAsync(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Domain.CategoryAggregate.Category()
        {
            Name = request.Name,
            ShopId = request.ShopId
        };
        category.SetCreationTracking(UserClaimsValue.Id);
        var entryEntity = await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entryEntity.Entity;
    }
}