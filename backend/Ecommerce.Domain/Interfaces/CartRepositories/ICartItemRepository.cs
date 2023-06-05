namespace Ecommerce.Domain.Interfaces.CartRepositories;

public interface ICartItemRepository
{
    Task<IEnumerable<CartItem>> GetAllAsync();
    Task<CartItem?> GetByIdAsync(int id);
    Task<CartItem> CreateAsync(CartItem cartItem);
    Task UpdateAsync(CartItem cartItem);
    Task DeleteAsync(CartItem cartItem);
}