namespace Ecommerce.Domain.Repositories.CartRepositories;

public interface ICartRepository
{
    Task<Cart?> GetByUserIdAsync(int userId);
    Cart Create(Cart cart);
}