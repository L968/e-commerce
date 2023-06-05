namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public Task<Product?> GetByGuidAsync(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<Product> CreateAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Product product)
    {
        throw new NotImplementedException();
    }
}