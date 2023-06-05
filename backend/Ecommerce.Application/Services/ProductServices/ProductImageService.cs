using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Services.ProductServices;

public class ProductImageService : IProductImageService
{
    private readonly IProductImageRepository _productImageRepository;
    private readonly IUploadFileService _uploadFileService;

    public ProductImageService(IProductImageRepository productImageRepository, IUploadFileService uploadFileService)
    {
        _productImageRepository = productImageRepository;
        _uploadFileService = uploadFileService;
    }

    public async Task<Result<List<ProductImage>>> UploadImages(int productId, IFormFileCollection files)
    {
        if (files.Count <= 0) return Result.Fail("No images sent");

        List<string> filePaths = _uploadFileService.UploadFiles(files);

        var productImages = new List<ProductImage>();

        foreach (var filePath in filePaths)
        {
            var productImage = new ProductImage(productId, filePath);

            await _productImageRepository.CreateAsync(productImage); // TODO: Aplicar/verificar transaction
        }

        return Result.Ok(productImages);
    }
}