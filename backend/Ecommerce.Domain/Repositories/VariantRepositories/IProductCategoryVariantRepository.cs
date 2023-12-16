using Ecommerce.Domain.Entities.VariantEntities;

namespace Ecommerce.Domain.Repositories.VariantRepositories;

public interface IProductCategoryVariantRepository
{
    Task<IEnumerable<ProductCategoryVariant>> GetByProductCategoryIdAsync(Guid id);
}
