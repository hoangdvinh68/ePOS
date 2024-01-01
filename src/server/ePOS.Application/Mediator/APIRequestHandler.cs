using ePOS.Application.Contracts;
using ePOS.Application.ValueObjects;
using MediatR;

namespace ePOS.Application.Mediator;

public abstract class APIRequestHandler<TRequest> : IRequestHandler<TRequest, APIResponse>
    where TRequest : IRequest<APIResponse>
{
    protected readonly IUserService UserService;
    protected readonly UserClaimsValue UserClaimsValue;
    
    protected APIRequestHandler(IUserService userService)
    {
        UserService = userService;
        UserClaimsValue = userService.GetUserClaimsValue();
    }
    
    public abstract Task<APIResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

public abstract class APIRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, APIResponse<TResponse>>
    where TRequest : IRequest<APIResponse<TResponse>>
{
    protected readonly IUserService UserService;
    protected readonly UserClaimsValue UserClaimsValue;
    
    protected APIRequestHandler(IUserService userService)
    {
        UserService = userService;
        UserClaimsValue = userService.GetUserClaimsValue();
    }

    public virtual async Task<APIResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var result = await HandleAsync(request, cancellationToken);
        return new APIResponse<TResponse>()
        {
            Success = true,
            Data = result,
            StatusCode = 200,
        };
    }

    protected abstract Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}