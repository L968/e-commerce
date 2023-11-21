using Ecommerce.Application.DTOs.OrderCheckout;
using Ecommerce.Order.API.RabbitMqClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Order.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "regular")]
public class OrderController : ControllerBase
{
    private readonly IRabbitMqClient _publisher;

    public OrderController(IRabbitMqClient publisher)
    {
        _publisher = publisher;
    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] OrderCheckoutDto orderCheckout)
    {
        orderCheckout.UserId = int.Parse(User.FindFirstValue("id")!);
        _publisher.PublishOrder(orderCheckout);
        return Ok();
    }
}
