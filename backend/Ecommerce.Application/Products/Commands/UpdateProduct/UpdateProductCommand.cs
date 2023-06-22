namespace Ecommerce.Application.Products.Commands.UpdateProduct;

[Authorize]
public record UpdateProductCommand : IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; set; }
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
    public Guid ProductCategoryGuid { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IProductCategoryRepository _productCategoryRepository;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository, IProductCategoryRepository productCategoryRepository)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _productCategoryRepository = productCategoryRepository;
    }

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null) return Result.Fail(DomainErrors.NotFound(nameof(Product), request.Id));

        ProductCategory? productCategory = await _productCategoryRepository.GetByGuidAsync(request.ProductCategoryGuid);
        if (productCategory is null) return Result.Fail(DomainErrors.NotFound(nameof(ProductCategory), request.ProductCategoryGuid));

        product.Update(
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
            productCategory.Id
        );

        _productRepository.Update(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}