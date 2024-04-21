using Ecommerce.Domain.Enums;
using Ecommerce.Order.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Order.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    [HttpGet("all")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] OrderStatus? status = null)
    {
        return Ok(await _orderService.GetAllAsync(page, pageSize, status));
    }

    [HttpGet]
    [Authorize(Roles = "regular")]
    public async Task<IActionResult> GetByUserId([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var userId = int.Parse(User.FindFirstValue("id")!);
        return Ok(await _orderService.GetByUserIdAsync(userId, page, pageSize));
    }

    [HttpGet("status-count")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetStatusCount()
    {
        return Ok(await _orderService.GetStatusCountAsync());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "regular")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = int.Parse(User.FindFirstValue("id")!);
        var order = await _orderService.GetByIdAsync(id, userId);

        if (order is null) return NotFound();

        return Ok(order);
    }

    [HttpPost]
    [Authorize(Roles = "regular")]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCheckoutDto orderCheckout)
    {
        orderCheckout.UserId = int.Parse(User.FindFirstValue("id")!);
        var result = await _orderService.CreateOrderAsync(orderCheckout);
        if (result.IsFailed)
        {
            return BadRequest(result.Reasons[0]);
        }

        return Ok(result.Value);
    }
}
