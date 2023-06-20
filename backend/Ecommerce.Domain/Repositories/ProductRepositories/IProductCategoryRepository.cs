namespace Ecommerce.Domain.Repositories.ProductRepositories;

public interface IProductCategoryRepository
{
    Task<IEnumerable<ProductCategory>> GetAllAsync();
    Task<ProductCategory?> GetByGuidAsync(Guid guid);
    ProductCategory Create(ProductCategory productCategory);
    void Update(ProductCategory productCategory);
    void Delete(ProductCategory productCategory);
}