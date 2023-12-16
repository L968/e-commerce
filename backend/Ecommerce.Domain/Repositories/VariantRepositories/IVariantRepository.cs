using Ecommerce.Domain.Entities.VariantEntities;

namespace Ecommerce.Domain.Repositories.VariantRepositories;

public interface IVariantRepository
{
    Task<IEnumerable<Variant>> GetAllAsync();
    Task<Variant?> GetByIdAsync(int id);
    Variant Create(Variant variant);
    void Update(Variant variant);
    void Delete(Variant variant);
}
