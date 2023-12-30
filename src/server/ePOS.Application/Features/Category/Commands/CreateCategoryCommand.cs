using ePOS.Application.Contracts;
using ePOS.Shared.Mediator;
using FluentValidation;

namespace ePOS.Application.Features.Category.Commands;

public class CreateCategoryCommand : IAPIRequest<Domain.CategoryAggregate.Category>
{
    public string Name { get; set; } = default!;
}

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class CreateCategoryCommandCommandHandle : APIRequestHandle<CreateCategoryCommand, Domain.CategoryAggregate.Category>
{
    private readonly ITenantContext _context;

    public CreateCategoryCommandCommandHandle(ITenantContext context)
    {
        _context = context;
    }

    protected override async Task<Domain.CategoryAggregate.Category> HandleAsync(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Domain.CategoryAggregate.Category()
        {
            Name = request.Name
        };
        category.SetCreationTracking(default);
        var entryEntity = await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entryEntity.Entity;
    }
}