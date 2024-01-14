using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.OrderCheckout;
using Ecommerce.Application.DTOs.Products;
using Ecommerce.Application.Features.Products.Queries;
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
    public async Task<IActionResult> Get()
    {
        var userId = int.Parse(User.FindFirstValue("id")!);
        return Ok(await _orderService.GetByUserIdAsync(userId));
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
    public IActionResult CreateOrder([FromBody] OrderCheckoutDto orderCheckout)
    {
        orderCheckout.UserId = int.Parse(User.FindFirstValue("id")!);
        _publisher.PublishOrder(orderCheckout);
        return Ok();
    }
}
