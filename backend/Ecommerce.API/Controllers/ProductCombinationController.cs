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
        GetProductCombinationDto productCombination = await _mediator.Send(command);
        return Ok(productCombination);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateProductCombinationCommand command)
    {
        command.Id = id;

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProductCombinationCommand(id));
        return NoContent();
    }

    [HttpPost("reduce-stock")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> ReduceStock(ReduceStockProductCombinationCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
