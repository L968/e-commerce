using Ecommerce.Domain.Entities.VariantEntities;

namespace Ecommerce.Domain.Repositories.VariantRepositories;

public interface IVariantRepository
{
    Task<IEnumerable<Variant>> GetAllAsync();
}
