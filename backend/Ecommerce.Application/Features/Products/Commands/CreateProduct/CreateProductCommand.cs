using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.Products.Commands.CreateProduct;

[Authorize]
public record CreateProductCommand : IRequest<Result<GetProductDto>>
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public Guid ProductCategoryId { get; set; }
    public bool? Active { get; set; }
    public bool? Visible { get; set; }
}

public class CreateProductCommandHandler(
    IMapper mapper, 
    IUnitOfWork unitOfWork,
    IProductRepository productRepository, 
    IProductCategoryRepository productCategoryRepository
    ) : IRequestHandler<CreateProductCommand, Result<GetProductDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;

    public async Task<Result<GetProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ProductCategory? productCategory = await _productCategoryRepository.GetByIdAsync(request.ProductCategoryId);
        if (productCategory is null) return Result.Fail(DomainErrors.NotFound(nameof(ProductCategory), request.ProductCategoryId));

        var createResult = Product.Create(
            request.Name,
            request.Description,
            request.Active!.Value,
            request.Visible!.Value,
            productCategory.Id
        );

        if (createResult.IsFailed) return Result.Fail(createResult.Errors);

        Product product = createResult.Value;

        _productRepository.Create(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var productDto = _mapper.Map<GetProductDto>(product);
        return Result.Ok(productDto);
    }
}
