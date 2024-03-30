using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Utils.Attributes;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.ProductCombinations.Commands.UpdateProductCombination;

[Authorize]
public record UpdateProductCombinationCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public List<int> VariantOptionIds { get; set; } = null!;
    public string Sku { get; set; } = "";
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public float Length { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }

    [DataType(DataType.Upload)]
    [MaxFileSize(5 * 1024 * 1024)]
    [AllowedExtensions([".jpg", ".png", ".webp"])]
    public IFormFileCollection Images { get; set; } = null!;
}

public class UpdateProductCombinationCommandHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IBlobStorageService blobStorageService,
    IProductRepository productRepository,
    IVariantOptionRepository variantOptionRepository,
    IProductCombinationRepository productCombinationRepository
    ) : IRequestHandler<UpdateProductCombinationCommand, Result>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IBlobStorageService _blobStorageService = blobStorageService;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IVariantOptionRepository _variantOptionRepository = variantOptionRepository;
    private readonly IProductCombinationRepository _productCombinationRepository = productCombinationRepository;

    public async Task<Result> Handle(UpdateProductCombinationCommand request, CancellationToken cancellationToken)
    {
        ProductCombination? productCombination = await _productCombinationRepository.GetByIdAsync(request.Id);
        if (productCombination is null) return Result.Fail(DomainErrors.NotFound(nameof(ProductCombination), request.Id));

        Product? product = await _productRepository.GetByIdAsync(productCombination.ProductId);
        if (product is null) return Result.Fail(DomainErrors.NotFound(nameof(Product), productCombination.ProductId));

        var variantOptions = new List<VariantOption>();

        foreach (var variantOptionId in request.VariantOptionIds)
        {
            VariantOption? variantOption = await _variantOptionRepository.GetByIdAsync(variantOptionId);
            if (variantOption is null) return Result.Fail(DomainErrors.NotFound(nameof(VariantOption), variantOptionId));

            variantOptions.Add(variantOption);
        }

        product.RemoveVariantOptionsByCombination(productCombination.Id);
        product.AddVariantOptions(variantOptions);

        await _blobStorageService.RemoveImage(productCombination.Images.Select(i => i.ImagePath));
        IEnumerable<string> imagePaths = await _blobStorageService.UploadImage(request.Images);

        var updateResult = productCombination.Update(
            variantOptions: variantOptions,
            sku: request.Sku,
            price: request.Price,
            length: request.Length,
            width: request.Width,
            height: request.Height,
            weight: request.Weight,
            imagePaths: imagePaths
        );

        if (updateResult.IsFailed) return updateResult;

        _productCombinationRepository.Update(productCombination);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
