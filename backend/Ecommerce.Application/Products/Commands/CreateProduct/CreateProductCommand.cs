using Ecommerce.Application.Products.Queries;
using Ecommerce.Utils.Attributes;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Products.Commands.CreateProduct;

[Authorize]
public record CreateProductCommand : IRequest<Result<GetProductDto>>
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Sku { get; set; } = "";
    public decimal Price { get; set; }
    public bool Active { get; set; }
    public bool Visible { get; set; }
    public float Length { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
    public int Stock { get; set; }
    public Guid ProductCategoryGuid { get; set; }

    [DataType(DataType.Upload)]
    [MaxFileSize(5 * 1024 * 1024)]
    [AllowedExtensions(new string[] { ".jpg", ".png" })]
    public IFormFileCollection Images { get; set; } = null!;
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<GetProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IProductCategoryRepository _productCategoryRepository;

    public CreateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository, IProductCategoryRepository productCategoryRepository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _productCategoryRepository = productCategoryRepository;
    }

    public async Task<Result<GetProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ProductCategory? productCategory = await _productCategoryRepository.GetByGuidAsync(request.ProductCategoryGuid);
        if (productCategory is null) return Result.Fail(DomainErrors.NotFound(nameof(ProductCategory), request.ProductCategoryGuid));

        Result<List<ProductImage>> uploadResult = await UploadProductImages(request.Images);
        if (uploadResult.IsFailed) return Result.Fail(uploadResult.Errors);

        var product = new Product(
            request.Name,
            request.Description,
            request.Sku,
            request.Price,
            request.Active,
            request.Visible,
            request.Length,
            request.Width,
            request.Height,
            request.Weight,
            productCategory.Id,
            request.Stock,
            images: uploadResult.Value
        );

        _productRepository.Create(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var productDto = _mapper.Map<GetProductDto>(product);
        return Result.Ok(productDto);
    }

    private async Task<Result<List<ProductImage>>> UploadProductImages(IFormFileCollection images)
    {
        return Result.Ok(new List<ProductImage>());
    }
}