namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductVariantRepository : IProductVariantRepository
{
    public Task<ProductVariant> CreateAsync(ProductVariant productVariant)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(ProductVariant productVariant)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductVariant>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductVariant?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(ProductVariant productVariant)
    {
        throw new NotImplementedException();
    }
}