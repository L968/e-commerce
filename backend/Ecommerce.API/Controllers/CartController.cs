using Ecommerce.Application.DTOs.Carts;
using Ecommerce.Application.Features.CartItems.Commands.CreateCartItem;
using Ecommerce.Application.Features.CartItems.Commands.DeleteCartItem;
using Ecommerce.Application.Features.CartItems.Commands.UpdateCartItemQuantity;
using Ecommerce.Application.Features.Carts.Commands;
using Ecommerce.Application.Features.Carts.Queries;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "regular")]
    public class CartController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetCartByUserIdQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            GetCartDto cart = await _mediator.Send(new CreateCartCommand());
            return CreatedAtAction(nameof(Get), cart);
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddCartItem([FromBody] CreateCartItemCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("remove-item/{cartItemId}")]
        public async Task<IActionResult> RemoveCartItem(int cartItemId)
        {
            await _mediator.Send(new DeleteCartItemCommand(cartItemId));
            return NoContent();
        }

        [HttpPatch("update-item-quantity/{cartItemId}")]
        public async Task<IActionResult> UpdateCartItemQuantity(int cartItemId, [FromBody] UpdateCartItemQuantityCommand command)
        {
            command.Id = cartItemId;

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("clear-items")]
        public async Task<IActionResult> ClearCartItems([FromBody] Guid[] productCombinationIds)
        {
            await _mediator.Send(new ClearCartItemsCommand(productCombinationIds));
            return NoContent();
        }
    }
}
