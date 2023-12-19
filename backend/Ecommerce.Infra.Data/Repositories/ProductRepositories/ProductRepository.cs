namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Combinations)
                .ThenInclude(pc => pc.Images)
            .Include(p => p.Reviews)
            .Include(p => p.Discounts)
            .Include(p => p.VariantOptions)
                .ThenInclude(pvo => pvo.VariantOption)
                .ThenInclude(vo => vo.Variant)
            .Where(p => p.Combinations.Any())
            .ToListAsync();
    }

    public override async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products
            .Include(p => p.Reviews)
            .Include(p => p.Discounts)
            .Include(p => p.Combinations)
                .ThenInclude(pc => pc.Inventory)
            .Include(p => p.Combinations)
                .ThenInclude(pc => pc.Images)
            .Include(p => p.VariantOptions)
                .ThenInclude(pvo => pvo.VariantOption)
                .ThenInclude(vo => vo.Variant)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
