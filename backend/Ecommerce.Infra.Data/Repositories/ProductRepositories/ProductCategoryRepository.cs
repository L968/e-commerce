namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductCategoryRepository : IProductCategoryRepository
{
    public Task<ProductCategory> CreateAsync(ProductCategory productCategory)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(ProductCategory productCategory)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductCategory>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductCategory?> GetByIdAsync(int? id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(ProductCategory productCategory)
    {
        throw new NotImplementedException();
    }
}