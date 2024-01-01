namespace Ecommerce.Infra.Data.Repositories.CartRepositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cart?> GetByUserIdAsync(int userId)
    {
        return await _context.Carts
            .Include(c => c.CartItems)
                .ThenInclude(ci => ci.ProductCombination)
                    .ThenInclude(pc => pc.Product)
                        .ThenInclude(p => p.Discounts)
            .Include(c => c.CartItems)
                .ThenInclude(ci => ci.ProductCombination)
                    .ThenInclude(pc => pc.Images)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public Cart Create(Cart cart)
    {
        _context.Carts.Add(cart);
        return cart;
    }
}
