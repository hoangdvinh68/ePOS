using ePOS.Application.Contracts;
using ePOS.Application.Features.User.Responses;
using ePOS.Application.Mediator;
using ePOS.Application.Utilities;
using FluentValidation;

namespace ePOS.Application.Features.User.Commands;

public class SignInCommand : IAPIRequest<SignInResponse>
{
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;
}

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().Matches(RegexUtils.EmailRegex);
        RuleFor(x => x.Password).NotEmpty();
    }
}

public class SignInCommandHandler : APIRequestHandler<SignInCommand, SignInResponse>
{
    public SignInCommandHandler(IUserService userService, ITenantContext context) : base(userService) { }

    protected override async Task<SignInResponse> HandleAsync(SignInCommand request,
        CancellationToken cancellationToken) => await UserService.SignInAsync(request, cancellationToken);
}