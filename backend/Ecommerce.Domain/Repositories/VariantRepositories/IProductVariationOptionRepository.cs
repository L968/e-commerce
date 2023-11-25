using Ecommerce.Domain.Entities.VariantEntities;

namespace Ecommerce.Domain.Repositories.VariantRepositories;

public interface IProductVariationOptionRepository
{
    Task<IEnumerable<ProductVariationOption>> GetAllAsync();
    Task<ProductVariationOption?> GetByIdAsync(int id);
    ProductVariationOption Create(ProductVariationOption productVariationOption);
    void Update(ProductVariationOption productVariationOption);
    void Delete(ProductVariationOption productVariationOption);
}
