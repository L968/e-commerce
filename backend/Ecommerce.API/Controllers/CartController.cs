using Ecommerce.Application.CartItems.Commands.CreateCartItem;
using Ecommerce.Application.Carts.Commands;
using Ecommerce.Application.Carts.Queries;
using MediatR;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "regular")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId()
        {
            return Ok(await _mediator.Send(new GetCartByUserIdQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            Result<GetCartDto> result = await _mediator.Send(new CreateCartCommand());

            if (result.IsFailed)
            {
                return BadRequest(result.Reasons);
            }

            return CreatedAtAction(nameof(GetByUserId), result.Value);
        }

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddCartItem([FromBody] CreateCartItemCommand command)
        {
            Result result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                return BadRequest(result.Reasons);
            }

            return NoContent();
        }
    }
}