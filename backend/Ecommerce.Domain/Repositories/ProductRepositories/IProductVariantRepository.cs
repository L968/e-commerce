namespace Ecommerce.Domain.Repositories.ProductRepositories;

public interface IProductVariantRepository
{
    Task<IEnumerable<ProductVariant>> GetAllAsync();
    Task<ProductVariant?> GetByIdAsync(int id);
    Task<ProductVariant> CreateAsync(ProductVariant productVariant);
    Task UpdateAsync(ProductVariant productVariant);
    Task DeleteAsync(ProductVariant productVariant);
}