namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductCategoryRepository(AppDbContext context) : BaseRepository<ProductCategory>(context), IProductCategoryRepository
{
    public override async Task<IEnumerable<ProductCategory>> GetAllAsync()
    {
        return await _context.ProductCategories
            .Include(pc => pc.Variants)
                .ThenInclude(pcv => pcv.Variant)
            .ToListAsync();
    }

    public override async Task<ProductCategory?> GetByIdAsync(Guid id)
    {
        return await _context.ProductCategories
            .Include(pc => pc.Variants)
                .ThenInclude(pcv => pcv.Variant)
            .FirstOrDefaultAsync(pc => pc.Id == id);
    }
}
