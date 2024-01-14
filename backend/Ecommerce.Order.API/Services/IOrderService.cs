using Ecommerce.Application.DTOs;

namespace Ecommerce.Order.API.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetPendingOrdersAsync();
    Task<IEnumerable<OrderDto>> GetByUserIdAsync(int userId);
    Task<OrderDto?> GetByIdAsync(Guid id);
    Task<OrderDto?> GetByIdAsync(Guid id, int userId);
}
