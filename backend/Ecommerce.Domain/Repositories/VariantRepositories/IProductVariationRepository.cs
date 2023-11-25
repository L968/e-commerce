using Ecommerce.Domain.Entities.VariantEntities;

namespace Ecommerce.Domain.Repositories.VariantRepositories;

public interface IProductVariationRepository
{
    Task<IEnumerable<ProductVariation>> GetAllAsync();
    Task<ProductVariation?> GetByIdAsync(int id);
    ProductVariation Create(ProductVariation productVariation);
    void Update(ProductVariation productVariation);
    void Delete(ProductVariation productVariation);
}
