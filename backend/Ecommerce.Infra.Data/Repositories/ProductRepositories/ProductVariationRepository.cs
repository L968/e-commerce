namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductVariationRepository : IProductVariationRepository
{
    public Task<ProductVariation> CreateAsync(ProductVariation productVariant)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(ProductVariation productVariant)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductVariation>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductVariation?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(ProductVariation productVariant)
    {
        throw new NotImplementedException();
    }
}
