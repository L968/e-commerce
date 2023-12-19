namespace Ecommerce.Infra.Data.Repositories.ProductRepositories;

public class ProductCombinationRepository : BaseRepository<ProductCombination>, IProductCombinationRepository
{
    public ProductCombinationRepository(AppDbContext context) : base(context)
    {
    }
}
