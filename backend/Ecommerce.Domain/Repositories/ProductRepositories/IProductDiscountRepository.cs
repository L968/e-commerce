using Ecommerce.Common.Infra.Repositories;

namespace Ecommerce.Domain.Repositories.ProductRepositories;

public interface IProductDiscountRepository : IBaseRepository<ProductDiscount>
{
    Task<IEnumerable<ProductDiscount>> GetByProductIdAsync(Guid productId);
}
