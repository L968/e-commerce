using Ecommerce.Application.Features.Orders.Commands.OrderCheckout;
using Ecommerce.Order.API.RabbitMqClient;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Order.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IRabbitMqClient _publisher;

    public OrderController(IRabbitMqClient publisher)
    {
        _publisher = publisher;
    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] OrderCheckoutCommand order)
    {
        _publisher.PublishOrder(order);
        return Ok();
    }
}