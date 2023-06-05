namespace Ecommerce.Infra.Data.Repositories.OrderRepositories;

public class OrderRepository : IOrderRepository
{
    public Task<Order> CreateAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Order order)
    {
        throw new NotImplementedException();
    }
}