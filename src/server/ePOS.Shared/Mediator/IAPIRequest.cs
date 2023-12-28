using ePOS.Shared.ValueObjects;
using MediatR;

namespace ePOS.Shared.Mediator;

public interface IAPIRequest : IRequest<APIResponse>
{
    
}

public interface IAPIRequest<TResponse> : IRequest<APIResponse<TResponse>>
{
    
}