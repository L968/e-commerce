namespace Ecommerce.Domain.Repositories.ProductRepositories;

public interface IProductCombinationRepository : IBaseRepository<ProductCombination>
{
    Task<bool> CombinationStringExistsAsync(Guid productId, string combinationString);
}
