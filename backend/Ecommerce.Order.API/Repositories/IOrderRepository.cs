namespace Ecommerce.Order.API.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Domain.Entities.OrderEntities.Order>> GetPendingOrdersAsync();
    Task<IEnumerable<Domain.Entities.OrderEntities.Order>> GetByUserIdAsync(int userId);
    Task<Domain.Entities.OrderEntities.Order?> GetByIdAsync(Guid id);
    Task<Domain.Entities.OrderEntities.Order?> GetByIdAsync(Guid id, int userId);
}
