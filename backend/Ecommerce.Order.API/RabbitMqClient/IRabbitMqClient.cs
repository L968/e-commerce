using Ecommerce.Application.Features.Orders.Commands.OrderCheckout;

namespace Ecommerce.Order.API.RabbitMqClient;

public interface IRabbitMqClient
{
    void PublishOrder(OrderCheckoutCommand order);
}