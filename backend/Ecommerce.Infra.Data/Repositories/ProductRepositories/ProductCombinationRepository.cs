namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductCombinationRepository : BaseRepository<ProductCombination>, IProductCombinationRepository
{
    public ProductCombinationRepository(AppDbContext context) : base(context)
    {
    }

    public async override Task<ProductCombination?> GetByIdAsync(Guid id)
    {
        return await _context.ProductCombinations
            .Include(pc => pc.Images)
            .FirstOrDefaultAsync(pc => pc.Id == id);
    }

    public Task<bool> CombinationStringExistsAsync(Guid productId, string combinationString)
    {
        return _context.ProductCombinations
            .AnyAsync(pc => pc.ProductId == productId && pc.CombinationString == combinationString);
    }
}
