namespace Ecommerce.Domain.Repositories.ProductRepositories;

public interface IProductVariationRepository
{
    Task<IEnumerable<ProductVariation>> GetAllAsync();
    Task<ProductVariation?> GetByIdAsync(int id);
    Task<ProductVariation> CreateAsync(ProductVariation productVariant);
    Task UpdateAsync(ProductVariation productVariant);
    Task DeleteAsync(ProductVariation productVariant);
}