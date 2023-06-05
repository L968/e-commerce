namespace Ecommerce.Infra.Data.Repositories.CartRepositories;

public class CartItemRepository : ICartItemRepository
{
    public Task<CartItem> CreateAsync(CartItem cartItem)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(CartItem cartItem)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CartItem>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CartItem?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(CartItem cartItem)
    {
        throw new NotImplementedException();
    }
}