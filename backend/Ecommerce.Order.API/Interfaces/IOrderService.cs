using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Order.API.Interfaces;

public interface IOrderService
{
    Task<Pagination<OrderDto>> GetAllAsync(int page, int pageSize, OrderStatus? status);
    Task<Pagination<OrderDto>> GetByUserIdAsync(int userId, int page, int pageSize);
    Task<OrderStatusCountDto> GetStatusCountAsync();
    Task<OrderDto?> GetByIdAsync(Guid id);
    Task<OrderDto?> GetByIdAsync(Guid id, int userId);
    Task<Result<string>> CreateOrderAsync(OrderCheckoutDto order);
    Task<Result> ProcessPayPalReturnAsync(string token);
    Task<Result> ProcessPayPalCancelAsync(string token);
}
