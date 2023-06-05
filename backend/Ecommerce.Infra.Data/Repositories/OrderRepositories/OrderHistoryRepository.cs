namespace Ecommerce.Infra.Data.Repositories.OrderRepositories;

public class OrderHistoryRepository : IOrderHistoryRepository
{
    public Task<OrderHistory> CreateAsync(OrderHistory orderHistory)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(OrderHistory orderHistory)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderHistory>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OrderHistory?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(OrderHistory orderHistory)
    {
        throw new NotImplementedException();
    }
}