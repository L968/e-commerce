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
        await _orderService.ProcessPayPalReturnAsync(tokenModel.token);
        return NoContent();
    }

    [HttpPost("cancel")]
    public async Task<IActionResult> Cancel([FromBody] TokenModel tokenModel)
    {
        await _orderService.ProcessPayPalCancelAsync(tokenModel.token);
        return NoContent();
    }
}
