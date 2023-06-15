namespace Ecommerce.Domain.Repositories.ProductRepositories;

public interface IProductCategoryRepository
{
    Task<IEnumerable<ProductCategory>> GetAllAsync();
    Task<ProductCategory?> GetByIdAsync(int? id);
    Task<ProductCategory> CreateAsync(ProductCategory productCategory);
    Task UpdateAsync(ProductCategory productCategory);
    Task DeleteAsync(ProductCategory productCategory);
}