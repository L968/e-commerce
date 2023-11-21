namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductCombinationRepository : IProductCombinationRepository
{
    private readonly AppDbContext _context;

    public ProductCombinationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductCombination>> GetAllAsync()
    {
        return await _context.ProductsCombination
            .Include(p => p.Images)
            .ToListAsync();
    }

    public async Task<ProductCombination?> GetByIdAsync(Guid id)
    {
        return await _context.ProductsCombination
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public ProductCombination Create(ProductCombination productCombination)
    {
        _context.ProductsCombination.Add(productCombination);
        return productCombination;
    }

    public void Update(ProductCombination productCombination)
    {
        _context.ProductsCombination.Update(productCombination);
    }

    public void Delete(ProductCombination productCombination)
    {
        _context.ProductsCombination.Remove(productCombination);
    }
}
