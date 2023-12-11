using Ecommerce.Application.Features.ProductCategories.Commands.CreateProductCategory;
using Ecommerce.Application.Features.ProductCategories.Commands.UpdateProductCategory;
using Ecommerce.Application.Features.ProductCategories.Queries;
using Ecommerce.Application.Features.ProductCategories.Commands.DeleteProductCategory;
using Ecommerce.Application.DTOs.Products;

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

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateProductCategoryCommand command)
        {
            GetProductCategoryDto productCategory = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), null, productCategory);
        }

        [HttpPut("{guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(Guid guid, [FromBody] UpdateProductCategoryCommand command)
        {
            command.Guid = guid;
            Result result = await _mediator.Send(command);

            if (result.IsFailed) return NotFound();

            return NoContent();
        }

        [HttpDelete("{guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            Result result = await _mediator.Send(new DeleteProductCategoryCommand(guid));

            if (result.IsFailed) return NotFound();

            return NoContent();
        }
    }
}
