namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductInventoryRepository : IProductInventoryRepository
{
    public Task<ProductInventory> CreateAsync(ProductInventory productInventory)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(ProductInventory productInventory)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductInventory>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductInventory?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(ProductInventory productInventory)
    {
        throw new NotImplementedException();
    }
}