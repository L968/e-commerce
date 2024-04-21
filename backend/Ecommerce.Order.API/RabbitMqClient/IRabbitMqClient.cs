using Ecommerce.Order.API.DTOs.OrderCheckout;

namespace Ecommerce.Order.API.RabbitMqClient;

public interface IRabbitMqClient
{
    void PublishOrder(OrderCheckoutDto order);
}
