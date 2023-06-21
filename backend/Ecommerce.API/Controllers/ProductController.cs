using Ecommerce.Application.Products.Commands.CreateProduct;
using Ecommerce.Application.Products.Commands.DeleteProduct;
using Ecommerce.Application.Products.Commands.UpdateProduct;
using Ecommerce.Application.Products.Queries;

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

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromForm] CreateProductCommand command)
        {
            Result<GetProductDto> result = await _mediator.Send(command);

            if (result.IsFailed) return BadRequest(result.Reasons);

            var product = result.Value;

            return CreatedAtAction(nameof(GetById), new { guid = product.Id }, product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
        {
            return NoContent();
            //command.Guid = guid;
            //Result result = await _mediator.Send(command);

            //if (result.IsFailed) return NotFound();

            //return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return NoContent();
            //Result result = await _mediator.Send(new DeleteProductCommand(guid));

            //if (result.IsFailed) return NotFound();

            //return NoContent();
        }
    }
}