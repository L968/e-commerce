using Ecommerce.Application.DTOs.Products;
using Ecommerce.Application.Features.ProductCombinations.Commands.AddProductCombination;
using Ecommerce.Application.Features.ProductCombinations.Commands.DeleteProductCombination;
using Ecommerce.Application.Features.ProductCombinations.Commands.UpdateProductCombination;
using Ecommerce.Application.Features.ProductCombinations.Queries;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductCombinationController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id}")]
    [Authorize(Roles = "regular")]
    public async Task<IActionResult> GetById(Guid id)
    {
        GetProductCombinationByIdDto? product = await _mediator.Send(new GetProductCombinationByIdQuery(id));

        if (product is null) return NotFound();

        return Ok(product);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromForm] CreateProductCombinationCommand command)
    {
        Result<GetProductCombinationDto> result = await _mediator.Send(command);

        if (result.IsFailed) return BadRequest(result.Reasons);

        return Ok(result.Value);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateProductCombinationCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);

        if (result.IsFailed)
        {
            if (result.Reasons[0].Message.Contains("not found"))
            {
                return NotFound();
            }

            return BadRequest(result.Reasons);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        Result result = await _mediator.Send(new DeleteProductCombinationCommand(id));

        if (result.IsFailed) return NotFound();

        return NoContent();
    }

    [HttpPost("reduce-stock")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> ReduceStock(ReduceStockProductCombinationCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailed)
            return BadRequest(result.Reasons);

        return NoContent();
    }
}
