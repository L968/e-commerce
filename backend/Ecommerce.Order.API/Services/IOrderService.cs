namespace Ecommerce.Order.API.Services;

public interface IOrderService
{
    Task<IEnumerable<Domain.Entities.OrderEntities.Order>> GetPendingOrdersAsync();
    Task<Domain.Entities.OrderEntities.Order?> GetByIdAsync(Guid id);
    void Update(Domain.Entities.OrderEntities.Order order);
}
