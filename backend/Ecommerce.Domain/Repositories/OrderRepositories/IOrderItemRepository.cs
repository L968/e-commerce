namespace Ecommerce.Domain.Repositories.OrderRepositories;

public interface IOrderItemRepository
{
    Task<IEnumerable<OrderItem>> GetAllAsync();
    Task<OrderItem?> GetByIdAsync(int id);
    Task<OrderItem> CreateAsync(OrderItem orderItem);
    Task UpdateAsync(OrderItem orderItem);
    Task DeleteAsync(OrderItem orderItem);
}