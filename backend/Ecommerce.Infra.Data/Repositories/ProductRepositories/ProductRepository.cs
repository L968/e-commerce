namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductRepository(AppDbContext context) : BaseRepository<AppDbContext, Product>(context), IProductRepository
{
    public async Task<(IEnumerable<Product> Products, long TotalItems)> GetAllAsync(int page, int pageSize)
    {
        var productsQuery = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Combinations)
                .ThenInclude(pc => pc.Images)
            .Include(p => p.Reviews)
            .Include(p => p.Discounts)
            .Include(p => p.VariantOptions)
                .ThenInclude(pvo => pvo.VariantOption)
                .ThenInclude(vo => vo.Variant)
            .Where(p => p.Combinations.Any());

        var totalItems = await productsQuery.CountAsync();

        var products = await productsQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (products, totalItems);
    }

    public async Task<IEnumerable<Product>> GetDraftsAsync()
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
            .Where(p => p.Combinations == null || !p.Combinations.Any())
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

    public async Task<Product?> GetWithCombinationsByIdAsync(Guid id)
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
            .Where(p => p.Combinations.Any())
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product?> GetByIdAdminAsync(Guid id)
    {
        return await _context.Products
            .Include(p => p.Combinations)
                .ThenInclude(pc => pc.Inventory)
            .Include(p => p.Combinations)
                .ThenInclude(pc => pc.Images)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
