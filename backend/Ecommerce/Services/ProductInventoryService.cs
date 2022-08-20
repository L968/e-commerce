namespace Ecommerce.Services
{
    public class ProductInventoryService
    {
        private readonly Context _context;

        public ProductInventoryService(Context context)
        {
            _context = context;
        }

        public IEnumerable<GetProductInventoryDto> Get()
        {
            return _context.ProductInventories
                .Include(productInventory => productInventory.Product)
                .Select(productInventory => productInventory.ToDto());
        }

        public GetProductInventoryDto? Get(long id)
        {
            return _context.ProductInventories
                .Include(productInventory => productInventory.Product)
                .FirstOrDefault(productInventory => productInventory.Id == id)?
                .ToDto();
        }

        public GetProductInventoryDto Create(CreateProductInventoryDto productDto)
        {
            var productInventory = new ProductInventory()
            {
                Quantity = productDto.Quantity,
                ProductId = productDto.ProductId,
            };

            _context.ProductInventories.Add(productInventory);
            _context.SaveChanges();

            return productInventory.ToDto();
        }

        public Result Update(long id, UpdateProductInventoryDto productDto)
        {
            ProductInventory? productInventory = _context.ProductInventories.FirstOrDefault(productInventory => productInventory.Id == id);

            if (productInventory == null)
            {
                return Result.Fail("ProductInventory not found");
            }

            productInventory.Quantity = productDto.Quantity;

            _context.SaveChanges();
            return Result.Ok();
        }

        public Result Delete(long id)
        {
            ProductInventory? productInventory = _context.ProductInventories.FirstOrDefault(productInventory => productInventory.Id == id);

            if (productInventory == null)
            {
                return Result.Fail("ProductInventory not found");
            }

            _context.Remove(productInventory);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}