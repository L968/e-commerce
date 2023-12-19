namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductImageRepository : BaseRepository<ProductImage>, IProductImageRepository
{
    public ProductImageRepository(AppDbContext context) : base(context)
    {
    }
}
