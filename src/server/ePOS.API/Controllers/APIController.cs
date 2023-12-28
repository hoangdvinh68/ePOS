using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ePOS.API.Controllers;

[ApiController]
[Route("api/v1")]
public class APIController : ControllerBase
{
    protected readonly IMediator Mediator;

    public APIController(IMediator mediator)
    {
        Mediator = mediator;
    }
}