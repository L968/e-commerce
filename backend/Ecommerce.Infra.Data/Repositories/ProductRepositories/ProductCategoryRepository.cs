namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly AppDbContext _context;

    public ProductCategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductCategory>> GetAllAsync()
    {
        return await _context.ProductCategories
            .Include(pc => pc.Variants)
                .ThenInclude(pcv => pcv.Variant)
            .ToListAsync();
    }

    public async Task<ProductCategory?> GetByIdAsync(Guid id)
    {
        return await _context.ProductCategories
            .Include(pc => pc.Variants)
                .ThenInclude(pcv => pcv.Variant)
            .FirstOrDefaultAsync(pc => pc.Id == id);
    }

    public ProductCategory Create(ProductCategory productCategory)
    {
        _context.ProductCategories.Add(productCategory);
        return productCategory;
    }

    public void Update(ProductCategory productCategory)
    {
        _context.ProductCategories.Update(productCategory);
    }

    public void Delete(ProductCategory productCategory)
    {
        _context.ProductCategories.Remove(productCategory);
    }
}
