using ePOS.Application.Features.FeatureCategory.Commands;
using ePOS.Domain.CategoryAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ePOS.API.Controllers;

[ApiController]
[Route("api/v1/category")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<Category>> Create([FromBody] CreateCategoryCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}