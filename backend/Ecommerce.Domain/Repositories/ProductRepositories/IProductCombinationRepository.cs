namespace Ecommerce.Domain.Repositories.ProductRepositories;

public interface IProductCombinationRepository
{
    Task<IEnumerable<ProductCombination>> GetAllAsync();
    Task<ProductCombination?> GetByIdAsync(Guid id);
    ProductCombination Create(ProductCombination productCombination);
    void Update(ProductCombination productCombination);
    void Delete(ProductCombination productCombination);
}
