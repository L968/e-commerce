namespace Ecommerce.Infra.Data.Repositories.OrderRepositories;

public class OrderItemRepository : IOrderItemRepository
{
    public Task<OrderItem> CreateAsync(OrderItem orderItem)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(OrderItem orderItem)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderItem>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OrderItem?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(OrderItem orderItem)
    {
        throw new NotImplementedException();
    }
}