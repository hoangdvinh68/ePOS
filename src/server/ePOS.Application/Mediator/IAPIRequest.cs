﻿using ePOS.Application.ValueObjects;
using MediatR;

namespace ePOS.Application.Mediator;

public interface IAPIRequest : IRequest<APIResponse>
{
    
}

public interface IAPIRequest<TResponse> : IRequest<APIResponse<TResponse>>
{
    
}