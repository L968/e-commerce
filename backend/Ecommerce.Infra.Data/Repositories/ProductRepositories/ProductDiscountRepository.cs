namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductDiscountRepository : IProductDiscountRepository
{
    private readonly AppDbContext _context;

    public ProductDiscountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDiscount>> GetAllAsync()
    {
        return await _context.ProductDiscounts.ToListAsync();
    }

    public async Task<IEnumerable<ProductDiscount>> GetByProductIdAsync(Guid productId)
    {
        return await _context.ProductDiscounts
            .Where(pd => pd.ProductId == productId)
            .ToListAsync();
    }

    public async Task<ProductDiscount?> GetByIdAsync(int id)
    {
        return await _context.ProductDiscounts.FirstOrDefaultAsync(pd => pd.Id == id);
    }

    public ProductDiscount Create(ProductDiscount productDiscount)
    {
        _context.ProductDiscounts.Add(productDiscount);
        return productDiscount;
    }

    public void Update(ProductDiscount productDiscount)
    {
        _context.ProductDiscounts.Update(productDiscount);
    }

    public void Delete(ProductDiscount productDiscount)
    {
        _context.ProductDiscounts.Remove(productDiscount);
    }
}