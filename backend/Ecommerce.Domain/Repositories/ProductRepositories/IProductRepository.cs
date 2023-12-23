namespace Ecommerce.Domain.Repositories.ProductRepositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<IEnumerable<Product>> GetDraftsAsync();
    Task<Product?> GetByIdAdminAsync(Guid id);
}
