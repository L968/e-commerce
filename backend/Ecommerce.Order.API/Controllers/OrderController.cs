using Ecommerce.Application.DTOs.OrderCheckout;
using Ecommerce.Order.API.RabbitMqClient;
using Ecommerce.Order.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Order.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "regular")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IRabbitMqClient _publisher;

    public OrderController(IOrderService orderService, IRabbitMqClient publisher)
    {
        _orderService = orderService;
        _publisher = publisher;
    }

    [HttpGet("pending")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetPendingOrders()
    {
        return Ok(await _orderService.GetPendingOrdersAsync());
    }

    [HttpGet]
    public async Task<IActionResult> GetUserOrders()
    {
        var userId = int.Parse(User.FindFirstValue("id")!);
        return Ok(await _orderService.GetUserOrders(userId));
    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] OrderCheckoutDto orderCheckout)
    {
        orderCheckout.UserId = int.Parse(User.FindFirstValue("id")!);
        _publisher.PublishOrder(orderCheckout);
        return Ok();
    }
}
