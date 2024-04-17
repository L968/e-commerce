using Ecommerce.Order.API.Models.PayPal;

namespace Ecommerce.Order.API.Interfaces;

public interface IPayPalService
{
    Task<GetOrderResponse?> GetOrderAsync(string orderId);
    Task<CreateOrderResponse> CreateOrderAsync(decimal totalAmount, string currencyCode);
}
