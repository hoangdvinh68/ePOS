using ePOS.Shared.ValueObjects;
using MediatR;

namespace ePOS.Shared.Mediator;

public abstract class APIRequestHandle<TRequest> : IRequestHandler<TRequest, APIResponse>
    where TRequest : IRequest<APIResponse>
{
    public abstract Task<APIResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

public abstract class APIRequestHandle<TRequest, TResponse> : IRequestHandler<TRequest, APIResponse<TResponse>>
    where TRequest : IRequest<APIResponse<TResponse>>
{
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