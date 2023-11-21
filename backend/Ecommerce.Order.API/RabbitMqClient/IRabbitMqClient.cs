using Ecommerce.Application.DTOs.OrderCheckout;

namespace Ecommerce.Order.API.RabbitMqClient;

public interface IRabbitMqClient
{
    void PublishOrder(OrderCheckoutDto order);
}
