using Ecommerce.Domain.Repositories.VariantRepositories;

namespace Ecommerce.Infra.Data.Repositories.VariantRepositories;

public class ProductCategoryVariantRepository : IProductCategoryVariantRepository
{
    private readonly AppDbContext _context;

    public ProductCategoryVariantRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductCategoryVariant>> GetByProductCategoryIdAsync(int id)
    {
        return await _context.ProductCategoryVariants
            .Include(pcv => pcv.Variant)
                .ThenInclude(v => v.Options)
            .Where(pcv => pcv.ProductCategoryId == id)
            .ToListAsync();
    }
}
