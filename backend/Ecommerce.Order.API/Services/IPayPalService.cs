using Ecommerce.Order.API.Models.PayPal;

namespace Ecommerce.Order.API.Services;

public interface IPayPalService
{
    Task<GetOrderResponse?> GetOrderAsync(string orderId);
    Task<CreateOrderResponse> CreateOrderAsync();
}
