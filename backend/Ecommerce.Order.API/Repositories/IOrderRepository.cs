namespace Ecommerce.Order.API.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Domain.Entities.OrderEntities.Order>> GetPendingOrdersAsync();
    Task<(IEnumerable<Domain.Entities.OrderEntities.Order>, long TotalItems)> GetByUserIdAsync(int userId, int page, int pageSize);
    Task<Domain.Entities.OrderEntities.Order?> GetByIdAsync(Guid id, int userId);
}
