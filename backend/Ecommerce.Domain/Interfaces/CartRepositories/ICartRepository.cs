namespace Ecommerce.Domain.Interfaces.CartRepositories;

public interface ICartRepository
{
    Task<IEnumerable<Cart>> GetAllAsync();
    Task<Cart?> GetByIdAsync(int id);
    Task<Cart> CreateAsync(Cart cart);
    Task UpdateAsync(Cart cart);
    Task DeleteAsync(Cart cart);
}