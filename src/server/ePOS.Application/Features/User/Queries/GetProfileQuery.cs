using ePOS.Application.Contracts;
using ePOS.Application.Features.User.Responses;
using ePOS.Application.Mediator;
using FluentValidation;

namespace ePOS.Application.Features.User.Queries;

public class GetProfileQuery : IAPIRequest<GetProfileResponse>
{
    public string Email { get; set; } = default!;
}

public class GetProfileQueryValidator : AbstractValidator<GetProfileQuery>
{
    public GetProfileQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
    }
}

public class GetProfileQueryHandler : APIRequestHandler<GetProfileQuery, GetProfileResponse>
{
    public GetProfileQueryHandler(IUserService userService) : base(userService){ }

    protected override Task<GetProfileResponse> HandleAsync(GetProfileQuery request,
        CancellationToken cancellationToken) => UserService.GetProfileAsync(request, cancellationToken);
}