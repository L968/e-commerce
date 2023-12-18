using Ecommerce.Application.DTOs.Products;
using Ecommerce.Application.Features.Products.Commands.AddProductCombination;
using Ecommerce.Application.Features.Products.Commands.AddProductImage;
using Ecommerce.Application.Features.Products.Commands.CreateProduct;
using Ecommerce.Application.Features.Products.Commands.DeleteProduct;
using Ecommerce.Application.Features.Products.Commands.RemoveProductImage;
using Ecommerce.Application.Features.Products.Commands.UpdateProduct;
using Ecommerce.Application.Features.Products.Queries;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetProductQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            GetProductDto? product = await _mediator.Send(new GetProductByIdQuery(id));

            if (product is null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            Result<GetProductDto> result = await _mediator.Send(command);

            if (result.IsFailed) return BadRequest(result.Reasons);

            var product = result.Value;

            return CreatedAtAction(nameof(GetById), new { product.Id }, product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
        {
            command.Id = id;
            Result result = await _mediator.Send(command);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpPost("{id}/add-combination")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddCombination(Guid id, [FromForm] AddProductCombinationCommand command)
        {
            command.ProductId = id;
            Result<GetProductCombinationDto> result = await _mediator.Send(command);

            if (result.IsFailed) return BadRequest(result.Reasons);

            return Ok(result.Value);
        }

        [HttpPatch("{productCombinationid}/add-image")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddImage(Guid productCombinationid, [FromForm] AddProductImageCommand command)
        {
            command.ProductCombinationId = productCombinationid;
            Result result = await _mediator.Send(command);

            if (result.IsFailed) return BadRequest(result.Reasons);

            return NoContent();
        }

        [HttpPatch("{productCombinationid}/remove-image/{productImageId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveImage(Guid productCombinationid, int productImageId)
        {
            Result result = await _mediator.Send(new RemoveProductImageCommand(productCombinationid, productImageId));

            if (result.IsFailed) return BadRequest(result.Reasons);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Result result = await _mediator.Send(new DeleteProductCommand(id));

            if (result.IsFailed) return NotFound();

            return NoContent();
        }
    }
}
