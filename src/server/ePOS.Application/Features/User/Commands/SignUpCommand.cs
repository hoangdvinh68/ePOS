using ePOS.Application.Contracts;
using ePOS.Application.Features.User.Responses;
using ePOS.Application.Mediator;
using ePOS.Application.Utilities;
using FluentValidation;

namespace ePOS.Application.Features.User.Commands;

public class SignUpCommand : IAPIRequest<SignUpResponse>
{
    public string TenantName { get; set; } = default!;
    
    public string Email { get; set; } = default!;

    public string FirstName { get; set; } = default!;
    
    public string LastName { get; set; } = default!;
    
    public string Password { get; set; } = default!;
}

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(x => x.TenantName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().Matches(RegexUtils.EmailRegex);
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}

public class SignUpCommandHandler : APIRequestHandler<SignUpCommand, SignUpResponse>
{
    private readonly IUserService _userService;

    public SignUpCommandHandler(IUserService userService, ITenantContext context) : base(userService)
    {
        _userService = userService;
    }

    protected override async Task<SignUpResponse> HandleAsync(SignUpCommand request,
        CancellationToken cancellationToken) => await _userService.SignUpAsync(request, cancellationToken);
}