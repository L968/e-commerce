using Ecommerce.Order.API.Repositories;

namespace Ecommerce.Order.API.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.OrderEntities.Order?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Domain.Entities.OrderEntities.Order>> GetUserOrders(int userId)
    {
        return await _repository.GetByUserIdAsync(userId);
    }

    public async Task<IEnumerable<Domain.Entities.OrderEntities.Order>> GetPendingOrdersAsync()
    {
        return await _repository.GetPendingOrdersAsync();
    }
}
