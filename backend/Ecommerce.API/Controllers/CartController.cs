﻿using Ecommerce.Application.DTOs.Carts;
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
            Result<GetCartDto> result = await _mediator.Send(new CreateCartCommand());

            if (result.IsFailed) return BadRequest(result.Reasons);

            return CreatedAtAction(nameof(Get), result.Value);
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddCartItem([FromBody] CreateCartItemCommand command)
        {
            Result result = await _mediator.Send(command);

            if (result.IsFailed) return BadRequest(result.Reasons);

            return NoContent();
        }

        [HttpDelete("remove-item/{cartItemId}")]
        public async Task<IActionResult> RemoveCartItem(int cartItemId)
        {
            Result result = await _mediator.Send(new DeleteCartItemCommand(cartItemId));

            if (result.IsFailed) return BadRequest(result.Reasons);

            return NoContent();
        }

        [HttpPatch("update-item-quantity/{cartItemId}")]
        public async Task<IActionResult> UpdateCartItemQuantity(int cartItemId, [FromBody] UpdateCartItemQuantityCommand command)
        {
            command.Id = cartItemId;
            Result result = await _mediator.Send(command);

            if (result.IsFailed) return BadRequest(result.Reasons);

            return NoContent();
        }

        [HttpPatch("clear-items")]
        public async Task<IActionResult> ClearCartItems([FromBody] Guid[] productCombinationIds)
        {
            Result result = await _mediator.Send(new ClearCartItemsCommand(productCombinationIds));

            if (result.IsFailed) return BadRequest(result.Reasons);

            return NoContent();
        }
    }
}
