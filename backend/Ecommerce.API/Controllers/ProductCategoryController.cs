using Ecommerce.Application.DTOs.Products;
using Ecommerce.Application.Features.ProductCategories.Commands.CreateProductCategory;
using Ecommerce.Application.Features.ProductCategories.Commands.DeleteProductCategory;
using Ecommerce.Application.Features.ProductCategories.Commands.UpdateProductCategory;
using Ecommerce.Application.Features.ProductCategories.Queries;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetProductCategoryQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            GetProductCategoryDto? productCategory = await _mediator.Send(new GetProductCategoryByIdQuery(id));

            if (productCategory is null) return NotFound();

            return Ok(productCategory);
        }

        [HttpGet("{id}/variants")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetVariants(Guid id)
        {
            var result = await _mediator.Send(new GetVariantsByProductCategoryIdQuery(id));

            if (result.IsFailed) return NotFound();

            return Ok(result.Value);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateProductCategoryCommand command)
        {
            GetProductCategoryDto productCategory = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), null, productCategory);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCategoryCommand command)
        {
            command.Id = id;
            Result result = await _mediator.Send(command);

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
            Result result = await _mediator.Send(new DeleteProductCategoryCommand(id));

            if (result.IsFailed) return NotFound();

            return NoContent();
        }
    }
}
