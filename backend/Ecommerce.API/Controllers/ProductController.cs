using Ecommerce.Application.DTOs.Products;
using Ecommerce.Application.Features.Products.Commands.CreateProduct;
using Ecommerce.Application.Features.Products.Commands.DeleteProduct;
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
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetProductQuery(page, pageSize);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("drafts")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetDrafts()
        {
            return Ok(await _mediator.Send(new GetProductDraftsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            GetProductDto? product = await _mediator.Send(new GetProductByIdQuery(id));

            if (product is null) return NotFound();

            return Ok(product);
        }

        [HttpGet("{id}/admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetByIdAdmin(Guid id)
        {
            GetProductAdminDto? product = await _mediator.Send(new GetProductByIdAdminQuery(id));

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
