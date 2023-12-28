﻿using ePOS.Shared.ValueObjects;
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
    public abstract Task<APIResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
}