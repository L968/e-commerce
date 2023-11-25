using Ecommerce.Domain.Entities.VariantEntities;

namespace Ecommerce.Domain.Repositories.VariantRepositories;

public interface IVariantOptionRepository
{
    Task<IEnumerable<VariantOption>> GetAllAsync();
    Task<VariantOption?> GetByIdAsync(int id);
    VariantOption Create(VariantOption variantOption);
    void Update(VariantOption variantOption);
    void Delete(VariantOption variantOption);
}
