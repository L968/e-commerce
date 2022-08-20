namespace Ecommerce.Services
{
    public class ProductImageService
    {
        private readonly Context _context;
        private readonly UploadFileService _uploadFileService;

        public ProductImageService(Context context, UploadFileService uploadFileService)
        {
            _context = context;
            _uploadFileService = uploadFileService;
        }

        public Result<List<ProductImage>> UploadImages(int productId, IFormFileCollection files)
        {
            if (files.Count <= 0) return Result.Fail("No images sent");

            var filePaths = _uploadFileService.UploadFiles(files);

            var productImages = new List<ProductImage>();

            foreach (var filePath in filePaths)
            {
                var productImage = new ProductImage()
                {
                    ImagePath = filePath,
                    ProductId = productId,
                };

                _context.ProductImages.Add(productImage);
                productImages.Add(productImage);
            }

            _context.SaveChanges();
            return Result.Ok(productImages);
        }
    }
}