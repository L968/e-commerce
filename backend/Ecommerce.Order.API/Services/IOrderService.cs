using Ecommerce.Application.DTOs;

namespace Ecommerce.Order.API.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetPendingOrdersAsync();
    Task<IEnumerable<OrderDto>> GetUserOrders(int userId);
    Task<OrderDto?> GetByIdAsync(Guid id);
}
