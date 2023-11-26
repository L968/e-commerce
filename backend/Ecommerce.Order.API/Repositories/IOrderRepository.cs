namespace Ecommerce.Order.API.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Domain.Entities.OrderEntities.Order>> GetPendingOrdersAsync();
    Task<Domain.Entities.OrderEntities.Order?> GetByIdAsync(Guid id);
}
