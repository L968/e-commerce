using Ecommerce.Application.DTOs.Products;
using Ecommerce.Application.Features.ProductCategories.Commands.CreateProductCategory;
using Ecommerce.Application.Features.ProductCategories.Commands.DeleteProductCategory;
using Ecommerce.Application.Features.ProductCategories.Commands.UpdateProductCategory;
using Ecommerce.Application.Features.ProductCategories.Queries;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCategoryController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

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
            var variants = await _mediator.Send(new GetVariantsByProductCategoryIdQuery(id));
            return Ok(variants);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateProductCategoryCommand command)
        {
            var productCategory = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), null, productCategory);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCategoryCommand command)
        {
            command.Id = id;

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteProductCategoryCommand(id));
            return NoContent();
        }
    }
}
