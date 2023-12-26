namespace ePOS.Application.Common.Mediator;

public interface IAPIRequest : IRequest<APIResponse>
{
    
}

public interface IAPIRequest<TResponse> : IRequest<APIResponse<TResponse>>
{
    
}