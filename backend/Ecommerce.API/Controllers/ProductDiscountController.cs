using Ecommerce.Application.DTOs.Products;
using Ecommerce.Application.Features.ProductDiscounts.Commands.CreateProductDiscount;
using Ecommerce.Application.Features.ProductDiscounts.Queries;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    public class ProductDiscountController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetProductDiscountsQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDiscountCommand command)
        {
            GetProductDiscountDto productDiscount = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), null, productDiscount);
        }
    }
}
