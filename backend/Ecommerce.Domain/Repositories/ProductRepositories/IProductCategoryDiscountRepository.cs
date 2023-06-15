namespace Ecommerce.Domain.Repositories.ProductRepositories;

public interface IProductCategoryDiscountRepository
{
    Task<IEnumerable<ProductCategoryDiscount>> GetAllAsync();
    Task<ProductCategoryDiscount?> GetByIdAsync(int id);
    Task<ProductCategoryDiscount> CreateAsync(ProductCategoryDiscount productCategoryDiscount);
    Task UpdateAsync(ProductCategoryDiscount productCategoryDiscount);
    Task DeleteAsync(ProductCategoryDiscount productCategoryDiscount);
}