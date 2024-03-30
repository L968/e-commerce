namespace Ecommerce.Infra.Data.Repositories.VariantRepositories;

public class ProductCategoryVariantRepository(AppDbContext context) : IProductCategoryVariantRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<ProductCategoryVariant>> GetByProductCategoryIdAsync(Guid id)
    {
        return await _context.ProductCategoryVariants
            .Include(pcv => pcv.Variant)
                .ThenInclude(v => v.Options)
            .Where(pcv => pcv.ProductCategoryId == id)
            .ToListAsync();
    }
}
