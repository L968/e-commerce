namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductImageRepository(AppDbContext context) : BaseRepository<AppDbContext, ProductImage>(context), IProductImageRepository
{
}
