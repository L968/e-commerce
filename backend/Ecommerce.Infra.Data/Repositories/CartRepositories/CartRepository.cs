namespace Ecommerce.Infra.Data.Repositories.CartRepositories;

public class CartRepository : ICartRepository
{
    public Task<Cart> CreateAsync(Cart cart)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Cart cart)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Cart>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Cart?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Cart cart)
    {
        throw new NotImplementedException();
    }
}