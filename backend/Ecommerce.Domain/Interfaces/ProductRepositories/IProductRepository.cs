namespace Ecommerce.Domain.Interfaces.ProductRepositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByGuidAsync(Guid guid);
    Task<Product> CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);
}