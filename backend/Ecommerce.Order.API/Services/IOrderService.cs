using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Order.API.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetPendingOrdersAsync();
    Task<Pagination<OrderDto>> GetByUserIdAsync(int userId, int page, int pageSize);
    Task<OrderDto?> GetByIdAsync(Guid id, int userId);
}
