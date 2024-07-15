using Ecommerce.Common.Infra.Representation;
using Ecommerce.Common.Infra.Representation.Grid;

namespace Ecommerce.Order.API.Interfaces;

public interface IOrderService
{
    Task<Pagination<OrderDto>> GetAllAsync(GridParams gridParams);
    Task<Pagination<OrderDto>> GetByUserIdAsync(int userId, int page, int pageSize);
    Task<OrderStatusCountDto> GetStatusCountAsync();
    Task<OrderDto?> GetByIdAsync(Guid id);
    Task<OrderDto?> GetByIdAsync(Guid id, int userId);
    Task<string> CreateOrderAsync(OrderCheckoutDto order);
    Task ProcessPayPalReturnAsync(string token);
    Task ProcessPayPalCancelAsync(string token);
}
