using Ecommerce.Application.Interfaces;
using Ecommerce.Utils.Attributes;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Products.Commands.AddProductImage;

[Authorize]
public record AddProductImageCommand : IRequest<Result>
{
    [JsonIgnore]
    public Guid ProductCombinationId { get; set; }

    [DataType(DataType.Upload)]
    [MaxFileSize(5 * 1024 * 1024)]
    [AllowedExtensions(new string[] { ".jpg", ".png" })]
    public IFormFile Image { get; set; } = null!;
}

public class AddProductImageCommandHandler : IRequestHandler<AddProductImageCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobStorageService _blobStorageService;
    private readonly IProductCombinationRepository _productCombinationRepository;

    public AddProductImageCommandHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService, IProductCombinationRepository productCombinationRepository)
    {
        _unitOfWork = unitOfWork;
        _blobStorageService = blobStorageService;
        _productCombinationRepository = productCombinationRepository;
    }

    public async Task<Result> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        ProductCombination? productCombination = await _productCombinationRepository.GetByIdAsync(request.ProductCombinationId);
        if (productCombination is null) return Result.Fail(DomainErrors.NotFound(nameof(Product), request.ProductCombinationId));

        var imagePath = await _blobStorageService.UploadImage(request.Image);

        productCombination.AddImage(imagePath);

        _productCombinationRepository.Update(productCombination);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
