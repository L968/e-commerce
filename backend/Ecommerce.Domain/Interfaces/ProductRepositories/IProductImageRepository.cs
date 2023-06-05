namespace Ecommerce.Domain.Interfaces.ProductRepositories;

public interface IProductImageRepository
{
    Task<IEnumerable<ProductImage>> GetAllAsync();
    Task<ProductImage?> GetByIdAsync(int id);
    Task<ProductImage> CreateAsync(ProductImage productImage);
    Task UpdateAsync(ProductImage productImage);
    Task DeleteAsync(ProductImage productImage);
}