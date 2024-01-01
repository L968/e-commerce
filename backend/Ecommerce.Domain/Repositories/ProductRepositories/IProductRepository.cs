namespace Ecommerce.Domain.Repositories.ProductRepositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<(IEnumerable<Product> Products, long TotalItems)> GetAllAsync(int page, int pageSize);
    Task<IEnumerable<Product>> GetDraftsAsync();
    Task<Product?> GetByIdAdminAsync(Guid id);
}
