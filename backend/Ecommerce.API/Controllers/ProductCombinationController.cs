using Ecommerce.Application.DTOs.Products;
using Ecommerce.Application.Features.ProductCombinations.Commands.AddProductCombination;
using Ecommerce.Application.Features.ProductCombinations.Commands.DeleteProductCombination;
using Ecommerce.Application.Features.ProductCombinations.Commands.UpdateProductCombination;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "admin")]
public class ProductCombinationController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductCombinationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateProductCombinationCommand command)
    {
        Result<GetProductCombinationDto> result = await _mediator.Send(command);

        if (result.IsFailed) return BadRequest(result.Reasons);

        return Ok(result.Value);
    }

    [HttpPut("{id}")]
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
    public async Task<IActionResult> Delete(Guid id)
    {
        Result result = await _mediator.Send(new DeleteProductCombinationCommand(id));

        if (result.IsFailed) return NotFound();

        return NoContent();
    }
}
