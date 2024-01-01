using ePOS.Application.Features.Business.Commands;
using ePOS.Domain.ItemAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ePOS.API.Controllers;

[ApiController]
[Route("api/v1/item")]
public class ItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<Item>> Create([FromBody] CreateItemCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}