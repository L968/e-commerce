namespace Ecommerce.Domain.Repositories.ProductRepositories;

public interface IProductDiscountRepository
{
    Task<IEnumerable<ProductDiscount>> GetAllAsync();
    Task<ProductDiscount?> GetByIdAsync(int id);
    Task<ProductDiscount> CreateAsync(ProductDiscount productDiscount);
    Task UpdateAsync(ProductDiscount productDiscount);
    Task DeleteAsync(ProductDiscount productDiscount);
}