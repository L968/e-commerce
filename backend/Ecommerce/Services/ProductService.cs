using System.Transactions;

namespace Ecommerce.Services
{
    public class ProductService
    {
        private readonly Context _context;
        private readonly ProductImageService _productImageService;

        public ProductService(Context context, ProductImageService productImageService)
        {
            _context = context;
            _productImageService = productImageService;
        }

        public IEnumerable<GetProductDto> Get()
        {
            return _context.Products
                .Include(product => product.ProductCategory)
                .Include(product => product.ProductInventory)
                .Include(product => product.Images)
                .Select(product => product.ToDto());
        }

        public GetProductDto? Get(Guid guid)
        {
            return _context.Products
                .Include(product => product.ProductCategory)
                .Include(product => product.ProductInventory)
                .Include(product => product.Images)
                .FirstOrDefault(product => product.Guid == guid)?
                .ToDto();
        }

        public Result<GetProductDto> Create(CreateProductDto productDto)
        {
            using var transaction = new TransactionScope();

            var product = new Product()
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Sku = productDto.Sku,
                Price = productDto.Price,
                Active = productDto.Active,
                Weight = productDto.Weight,
                ProductCategoryId = productDto.ProductCategoryId,
            };

            ProductCategory? productCategory = _context.ProductCategories.FirstOrDefault(productCategory => productCategory.Id == product.ProductCategoryId);

            if (productCategory == null)  return Result.Fail("Product category not found");

            _context.Products.Add(product);
            _context.SaveChanges();

            var result = _productImageService.UploadImages(product.Id!.Value, productDto.Images!);

            if (result.IsFailed) return Result.Fail(result.Errors);

            product.Images = result.Value;

            transaction.Complete();
            return Result.Ok(product.ToDto());
        }

        public Result Update(Guid guid, UpdateProductDto productDto)
        {
            Product? product = _context.Products.FirstOrDefault(product => product.Guid == guid);

            if (product == null)
            {
                return Result.Fail("Product not found");
            }

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Sku = productDto.Sku;
            product.Weight = productDto.Weight;
            product.ProductCategoryId = productDto.ProductCategoryId;
            product.Price = productDto.Price;

            _context.SaveChanges();
            return Result.Ok();
        }

        public Result Delete(Guid guid)
        {
            throw new NotImplementedException("Block if product has orders");
            Product? product = _context.Products.FirstOrDefault(product => product.Guid == guid);

            if (product == null)
            {
                return Result.Fail("Product not found");
            }

            _context.Remove(product);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}