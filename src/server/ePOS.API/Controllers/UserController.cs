using ePOS.Application.Features.User.Commands;
using ePOS.Application.Features.User.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ePOS.API.Controllers;

[ApiController]
[Route("api/v1/user")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<SignInResponse>> SignIn([FromBody] SignInCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    
    [HttpPost("sign-up")]
    public async Task<ActionResult<SignUpResponse>> SignUp([FromBody] SignUpCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}