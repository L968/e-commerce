namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductImageRepository(AppDbContext context) : BaseRepository<ProductImage>(context), IProductImageRepository
{
}
