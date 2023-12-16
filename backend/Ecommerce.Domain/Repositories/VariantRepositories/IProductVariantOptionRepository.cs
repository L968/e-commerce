using Ecommerce.Domain.Entities.VariantEntities;

namespace Ecommerce.Domain.Repositories.VariantRepositories;

public interface IProductVariantOptionRepository
{
    Task<IEnumerable<ProductVariantOption>> GetAllAsync();
    Task<ProductVariantOption?> GetByIdAsync(int id);
    ProductVariantOption Create(ProductVariantOption productVariantOption);
    void Update(ProductVariantOption productVariantOption);
    void Delete(ProductVariantOption productVariantOption);
}
