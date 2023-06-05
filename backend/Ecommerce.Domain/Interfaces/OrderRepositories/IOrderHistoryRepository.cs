namespace Ecommerce.Domain.Interfaces.OrderRepositories;

public interface IOrderHistoryRepository
{
    Task<IEnumerable<OrderHistory>> GetAllAsync();
    Task<OrderHistory?> GetByIdAsync(int id);
    Task<OrderHistory> CreateAsync(OrderHistory orderHistory);
    Task UpdateAsync(OrderHistory orderHistory);
    Task DeleteAsync(OrderHistory orderHistory);
}