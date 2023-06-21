namespace Ecommerce.Domain.Repositories.ProductRepositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Product Create(Product product);
    void Update(Product product);
    void Delete(Product product);
}