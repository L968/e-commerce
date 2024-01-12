namespace Ecommerce.Order.API.Services;

public interface IOrderService
{
    Task<IEnumerable<Domain.Entities.OrderEntities.Order>> GetPendingOrdersAsync();
    Task<IEnumerable<Domain.Entities.OrderEntities.Order>> GetUserOrders(int userId);
    Task<Domain.Entities.OrderEntities.Order?> GetByIdAsync(Guid id);
}
