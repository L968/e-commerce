using Ecommerce.Application.Features.Products.Commands.AddProductImage;
using Ecommerce.Application.Features.Products.Commands.CreateProduct;
using Ecommerce.Application.Features.Products.Commands.UpdateProduct;
using Ecommerce.Application.Features.Products.Queries;
using Ecommerce.Application.Features.Products.Commands.DeleteProduct;
using Ecommerce.Application.Features.Products.Commands.RemoveProductImage;
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
        public async Task<IActionResult> Create([FromForm] CreateProductCommand command)
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

        [HttpPatch("{id}/add-image")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddImage(Guid id, [FromForm] AddProductImageCommand command)
        {
            command.Id = id;
            Result result = await _mediator.Send(command);

            if (result.IsFailed) return BadRequest(result.Reasons);

            return NoContent();
        }

        [HttpPatch("{id}/remove-image/{productImageId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveImage(Guid id, int productImageId)
        {
            Result result = await _mediator.Send(new RemoveProductImageCommand(id, productImageId));

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