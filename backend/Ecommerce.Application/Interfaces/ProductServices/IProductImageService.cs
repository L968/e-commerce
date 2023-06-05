using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Interfaces.ProductServices;

public interface IProductImageService
{
    public Task<Result<List<ProductImage>>> UploadImages(int productId, IFormFileCollection files);
}