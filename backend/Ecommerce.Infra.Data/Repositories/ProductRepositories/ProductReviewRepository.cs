namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductReviewRepository : BaseRepository<ProductReview>, IProductReviewRepository
{
    public ProductReviewRepository(AppDbContext context) : base(context)
    {
    }
}
