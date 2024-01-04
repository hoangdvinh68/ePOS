using ePOS.Application.Features.Business.Queries;
using ePOS.Domain.TenantAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ePOS.API.Controllers;

[ApiController]
[Route("api/v1/tenant")]
public class TenantController : ControllerBase
{
    private readonly IMediator _mediator;

    public TenantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get")]
    public async Task<ActionResult<Tenant>> Get([FromQuery] GetTenantQuery query)
    {
        return Ok(await _mediator.Send(query));
    }
}