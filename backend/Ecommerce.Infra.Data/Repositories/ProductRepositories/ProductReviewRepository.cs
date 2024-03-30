namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductReviewRepository(AppDbContext context) : BaseRepository<ProductReview>(context), IProductReviewRepository
{
}
