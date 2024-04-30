namespace Ecommerce.Application.Features.Products.Commands.UpdateProduct;

[Authorize]
public record UpdateProductCommand : IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public bool? Active { get; set; }
    public bool? Visible { get; set; }
    public Guid ProductCategoryId { get; set; }
}

public class UpdateProductCommandHandler(
    IUnitOfWork unitOfWork,
    IProductRepository productRepository,
    IProductCategoryRepository productCategoryRepository
    ) : IRequestHandler<UpdateProductCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null) return DomainErrors.NotFound(nameof(Product), request.Id);

        ProductCategory? productCategory = await _productCategoryRepository.GetByIdAsync(request.ProductCategoryId);
        if (productCategory is null) return DomainErrors.NotFound(nameof(ProductCategory), request.ProductCategoryId);

        var updateResult = product.Update(
            request.Name,
            request.Description,
            request.Active!.Value,
            request.Visible!.Value,
            productCategory.Id
        );

        if (updateResult.IsFailed) return updateResult;

        _productRepository.Update(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
