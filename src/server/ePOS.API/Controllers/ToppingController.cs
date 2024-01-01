using ePOS.Application.Features.Business.Commands;
using ePOS.Domain.ToppingAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ePOS.API.Controllers;

[ApiController]
[Route("api/v1/topping")]
public class ToppingController : ControllerBase
{
    private readonly IMediator _mediator;

    public ToppingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<Topping>> Create([FromBody] CreateToppingCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}