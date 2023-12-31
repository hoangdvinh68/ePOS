using ePOS.Application.Contracts;
using ePOS.Application.Features.User.Responses;
using ePOS.Application.Mediator;
using ePOS.Shared.Utils;
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

public class SignInCommandHandle : APIRequestHandle<SignInCommand, SignInResponse>
{
    private readonly IUserService _userService;

    public SignInCommandHandle(IUserService userService, ITenantContext context) : base(userService)
    {
        _userService = userService;
    }

    protected override async Task<SignInResponse> HandleAsync(SignInCommand request,
        CancellationToken cancellationToken) => await _userService.SignInAsync(request, cancellationToken);
}