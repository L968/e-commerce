namespace Ecommerce.Domain.Repositories.ProductRepositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product?> GetByIdAdminAsync(Guid id);
}
