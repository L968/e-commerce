namespace Ecommerce.Domain.Interfaces.ProductRepositories;

public interface IProductInventoryRepository
{
    Task<IEnumerable<ProductInventory>> GetAllAsync();
    Task<ProductInventory?> GetByIdAsync(int id);
    Task<ProductInventory> CreateAsync(ProductInventory productInventory);
    Task UpdateAsync(ProductInventory productInventory);
    Task DeleteAsync(ProductInventory productInventory);
}