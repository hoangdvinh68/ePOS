using ePOS.Application.Features.Business.Queries;
using ePOS.Domain.ShopAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ePOS.API.Controllers;

[ApiController]
[Route("api/v1/shop")]
public class ShopController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShopController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<Shop>>> List()
    {
        return Ok(await _mediator.Send(new ListShopQuery()));
    }
}