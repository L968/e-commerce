using Ecommerce.Models.ProductModels;

namespace Ecommerce.Services
{
    public class ProductCategoryService
    {
        private readonly Context _context;

        public ProductCategoryService(Context context)
        {
            _context = context;
        }

        public IEnumerable<GetProductCategoryDto> Get()
        {
            return _context.ProductCategories
                .Include(productCategory => productCategory.Products)
                .Select(productCategory => productCategory.ToDto());
        }

        public GetProductCategoryDto? Get(Guid guid)
        {
            return _context.ProductCategories
                .Include(productCategory => productCategory.Products)
                .FirstOrDefault(productCategory => productCategory.Guid == guid)?
                .ToDto();
        }

        public GetProductCategoryDto Create(CreateProductCategoryDto productCategoryDto)
        {
            var productCategory = new ProductCategory()
            {
                Name = productCategoryDto.Name,
                Description = productCategoryDto.Description,
            };

            _context.ProductCategories.Add(productCategory);
            _context.SaveChanges();

            return productCategory.ToDto();
        }

        public Result Update(Guid guid, UpdateProductCategoryDto productCategoryDto)
        {
            ProductCategory? productCategory = _context.ProductCategories.FirstOrDefault(productCategory => productCategory.Guid == guid);

            if (productCategory == null)
            {
                return Result.Fail("Product category not found");
            }

            productCategory.Name = productCategoryDto.Name;
            productCategory.Description = productCategoryDto.Description;

            _context.SaveChanges();
            return Result.Ok();
        }

        public Result Delete(Guid guid)
        {
            throw new NotImplementedException("Block if category has products");

            ProductCategory? productCategory = _context.ProductCategories.FirstOrDefault(productCategory => productCategory.Guid == guid);

            if (productCategory == null)
            {
                return Result.Fail("Product category not found");
            }

            _context.Remove(productCategory);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}