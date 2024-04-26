namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductDiscountRepository(AppDbContext context) : BaseRepository<AppDbContext, ProductDiscount>(context), IProductDiscountRepository
{
    public async Task<IEnumerable<ProductDiscount>> GetByProductIdAsync(Guid productId)
    {
        return await _context.ProductDiscounts
            .Where(pd => pd.ProductId == productId)
            .ToListAsync();
    }
}
