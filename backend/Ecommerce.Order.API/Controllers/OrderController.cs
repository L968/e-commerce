using Ecommerce.Application.DTOs.OrderCheckout;
using Ecommerce.Order.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Order.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "regular")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    [HttpGet("pending")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetPendingOrders()
    {
        return Ok(await _orderService.GetPendingOrdersAsync());
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var userId = int.Parse(User.FindFirstValue("id")!);
        return Ok(await _orderService.GetByUserIdAsync(userId, page, pageSize));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = int.Parse(User.FindFirstValue("id")!);
        var order = await _orderService.GetByIdAsync(id, userId);

        if (order is null) return NotFound();

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCheckoutDto orderCheckout)
    {
        orderCheckout.UserId = int.Parse(User.FindFirstValue("id")!);
        string response = await _orderService.CreateOrderAsync(orderCheckout);
        return Ok(response);
    }
}
