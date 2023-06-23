using Ecommerce.Utils.Attributes;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Products.Commands.AddProductImage;

[Authorize]
public record AddProductImageCommand : IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [DataType(DataType.Upload)]
    [MaxFileSize(5 * 1024 * 1024)]
    [AllowedExtensions(new string[] { ".jpg", ".png" })]
    public IFormFile Image { get; set; } = null!;
}

public class AddProductImageCommandHandler : IRequestHandler<AddProductImageCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;

    public AddProductImageCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    public async Task<Result> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null) return Result.Fail(DomainErrors.NotFound(nameof(Product), request.Id));

        var uploadResult = await UploadProductImage(product.Id, request.Image);
        if (uploadResult.IsFailed) return Result.Fail(uploadResult.Errors);

        product.AddImage(image: uploadResult.Value);

        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }

    private async Task<Result<ProductImage>> UploadProductImage(Guid productId, IFormFile image)
    {
        // TODO: Add to upload system
        return Result.Ok(new ProductImage(productId, "test.png"));
    }
}