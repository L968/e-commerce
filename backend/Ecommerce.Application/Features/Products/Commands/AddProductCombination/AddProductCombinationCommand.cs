using Ecommerce.Application.Features.Products.Queries;
using Ecommerce.Utils.Attributes;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Products.Commands.AddProductCombination;

[Authorize]
public record AddProductCombinationCommand : IRequest<Result<GetProductCombinationDto>>
{
    public Guid ProductId { get; set; }
    public string CombinationString { get; set; } = "";
    public string Sku { get; set; } = "";
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public float Length { get; private set; }
    public float Width { get; private set; }
    public float Height { get; private set; }
    public float Weight { get; private set; }

    [DataType(DataType.Upload)]
    [MaxFileSize(5 * 1024 * 1024)]
    [AllowedExtensions(new string[] { ".jpg", ".png" })]
    public IFormFileCollection Images { get; set; } = null!;
}

public class AddProductCombinationCommandHandler : IRequestHandler<AddProductCombinationCommand, Result<GetProductCombinationDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IProductCombinationRepository _productCombinationRepository;

    public AddProductCombinationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository, IProductCombinationRepository productCombinationRepository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _productCombinationRepository = productCombinationRepository;
    }

    public async Task<Result<GetProductCombinationDto>> Handle(AddProductCombinationCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product is null) return Result.Fail(DomainErrors.NotFound(nameof(Product), request.ProductId));

        List<ProductCombination> productCombinations = new();

        Result<List<ProductImage>> uploadResult = await UploadProductImages(request.Images);
        if (uploadResult.IsFailed) return Result.Fail(uploadResult.Errors);

        var createResult = ProductCombination.Create(
            combinationString: request.CombinationString,
            sku: request.Sku,
            price: request.Price,
            stock: request.Stock,
            length: request.Length,
            width: request.Width,
            height: request.Height,
            weight: request.Weight,
            images: uploadResult.Value
        );

        if (createResult.IsFailed) return Result.Fail(createResult.Errors);

        ProductCombination productCombination = createResult.Value;

        _productCombinationRepository.Create(productCombination);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var productDto = _mapper.Map<GetProductCombinationDto>(product);
        return Result.Ok(productDto);
    }

    private async Task<Result<List<ProductImage>>> UploadProductImages(IFormFileCollection images)
    {
        return Result.Ok(new List<ProductImage>());
    }
}
