using ePOS.Application.Features.Business.Commands;
using ePOS.Application.Features.Business.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Unit = ePOS.Domain.UnitAggregate.Unit;

namespace ePOS.API.Controllers;

[ApiController]
[Route("api/v1/unit")]
public class UnitController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UnitController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<Unit>>> List()
    {
        return Ok(await _mediator.Send(new ListUnitQuery()));
    } 

    [HttpPost("create")]
    public async Task<ActionResult<Unit>> Create([FromBody] CreateUnitCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<Unit>> Update([FromBody] UpdateUnitCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}