namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductDiscountRepository : BaseRepository<ProductDiscount>, IProductDiscountRepository
{
    public ProductDiscountRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ProductDiscount>> GetByProductIdAsync(Guid productId)
    {
        return await _context.ProductDiscounts
            .Where(pd => pd.ProductId == productId)
            .ToListAsync();
    }
}
