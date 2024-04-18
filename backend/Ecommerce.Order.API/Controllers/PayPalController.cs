using Ecommerce.Order.API.Interfaces;
using Ecommerce.Order.API.Models.PayPal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Order.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "regular")]
public class PayPalController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    [HttpPost("return")]
    public async Task<IActionResult> Return([FromBody] TokenModel tokenModel)
    {
        var result = await _orderService.ProcessPayPalReturnAsync(tokenModel.token);
        if (result.IsFailed) return BadRequest(result.Reasons[0]);

        return NoContent();
    }

    [HttpPost("cancel")]
    public async Task<IActionResult> Cancel([FromBody] TokenModel tokenModel)
    {
        var result = await _orderService.ProcessPayPalCancelAsync(tokenModel.token);
        if (result.IsFailed) return BadRequest(result.Reasons[0]);

        return NoContent();
    }
}
