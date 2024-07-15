using Ecommerce.Application.DTOs.Products;
using Ecommerce.Common.Infra.Attributes;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.ProductCombinations.Commands.AddProductCombination;

[Authorize]
public record CreateProductCombinationCommand : IRequest<GetProductCombinationDto>
{
    public Guid ProductId { get; set; }
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

public class CreateProductCombinationCommandHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IBlobStorageService blobStorageService,
    IProductRepository productRepository,
    IVariantOptionRepository variantOptionRepository,
    IProductCombinationRepository productCombinationRepository
    ) : IRequestHandler<CreateProductCombinationCommand, GetProductCombinationDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IBlobStorageService _blobStorageService = blobStorageService;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IVariantOptionRepository _variantOptionRepository = variantOptionRepository;
    private readonly IProductCombinationRepository _productCombinationRepository = productCombinationRepository;

    public async Task<GetProductCombinationDto> Handle(CreateProductCombinationCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(request.ProductId);
        DomainException.ThrowIfNull(product, request.ProductId);

        var variantOptions = new List<VariantOption>();

        foreach (var variantOptionId in request.VariantOptionIds)
        {
            VariantOption? variantOption = await _variantOptionRepository.GetByIdAsync(variantOptionId);
            DomainException.ThrowIfNull(variantOption, variantOptionId);

            variantOptions.Add(variantOption);
        }

        IEnumerable<string> imagePaths = await _blobStorageService.UploadImage(request.Images); // TODO: Treat if create method throws an error (undo?)

        var productCombination = product.AddCombination(
            variantOptions: variantOptions,
            sku: request.Sku,
            price: request.Price,
            stock: request.Stock,
            length: request.Length,
            width: request.Width,
            height: request.Height,
            weight: request.Weight,
            imagePaths: imagePaths
        );

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GetProductCombinationDto>(productCombination);
    }
}
