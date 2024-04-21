using Ecommerce.Domain.Enums;

namespace Ecommerce.Order.API.Repositories;

public interface IOrderRepository
{
    Task<(IEnumerable<Domain.Entities.OrderEntities.Order>, long TotalItems)> GetAllAsync(int page, int pageSize, OrderStatus? status);
    Task<(IEnumerable<Domain.Entities.OrderEntities.Order>, long TotalItems)> GetByUserIdAsync(int userId, int page, int pageSize);
    Task<OrderStatusCountDto> GetStatusCountAsync();
    Task<Domain.Entities.OrderEntities.Order?> GetByIdAsync(Guid id, int userId);
    Task<Domain.Entities.OrderEntities.Order?> GetByExternalPaymentIdAsync(string externalPaymentId);
    Task CreateAsync(Domain.Entities.OrderEntities.Order order);
    Task UpdateAsync(Domain.Entities.OrderEntities.Order order);
}
