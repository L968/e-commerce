namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductReviewRepository(AppDbContext context) : BaseRepository<AppDbContext, ProductReview>(context), IProductReviewRepository
{
}
