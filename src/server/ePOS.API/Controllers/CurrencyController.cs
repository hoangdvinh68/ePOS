using ePOS.Application.Features.Business.Queries;
using ePOS.Domain.CurrencyAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ePOS.API.Controllers;

[ApiController]
[Route("api/v1/currency")]
public class CurrencyController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrencyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<Currency>>> List()
    {
        return Ok(await _mediator.Send(new ListCurrencyQuery()));
    }
}