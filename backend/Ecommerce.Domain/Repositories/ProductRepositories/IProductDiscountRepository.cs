namespace Ecommerce.Domain.Repositories.ProductRepositories;

public interface IProductDiscountRepository
{
    Task<IEnumerable<ProductDiscount>> GetAllAsync();
    Task<IEnumerable<ProductDiscount>> GetByProductIdAsync(Guid productId);
    Task<ProductDiscount?> GetByIdAsync(int id);
    ProductDiscount Create(ProductDiscount productDiscount);
    void Update(ProductDiscount productDiscount);
    void Delete(ProductDiscount productDiscount);
}