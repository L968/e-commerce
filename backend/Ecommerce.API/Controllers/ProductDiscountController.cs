using Ecommerce.Application.DTOs.Products;
using Ecommerce.Application.Features.ProductDiscounts.Commands.CreateProductDiscount;
using Ecommerce.Application.Features.ProductDiscounts.Queries;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    public class ProductDiscountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductDiscountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetProductDiscountsQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDiscountCommand command)
        {
            Result<GetProductDiscountDto> result = await _mediator.Send(command);
            if (result.IsFailed) return BadRequest(result.Reasons);

            var productDiscount = result.Value;
            return CreatedAtAction(nameof(Get), null, productDiscount);
        }
    }
}