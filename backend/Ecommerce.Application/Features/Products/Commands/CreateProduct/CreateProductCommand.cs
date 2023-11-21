using Ecommerce.Application.Features.Products.Queries;

namespace Ecommerce.Application.Features.Products.Commands.CreateProduct;

[Authorize]
public record CreateProductCommand : IRequest<Result<GetProductDto>>
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public bool Active { get; set; }
    public bool Visible { get; set; }
    public Guid ProductCategoryGuid { get; set; }
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

        var createResult = Product.Create(
            request.Name,
            request.Description,
            request.Active,
            request.Visible,
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
