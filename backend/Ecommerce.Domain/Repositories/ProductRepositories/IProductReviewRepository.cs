namespace Ecommerce.Domain.Repositories.ProductRepositories;

public interface IProductReviewRepository
{
    Task<IEnumerable<ProductReview>> GetAllAsync();
    Task<ProductReview?> GetByIdAsync(int id);
    Task<ProductReview> CreateAsync(ProductReview productReview);
    Task UpdateAsync(ProductReview productReview);
    Task DeleteAsync(ProductReview productReview);
}
