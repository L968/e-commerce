namespace Ecommerce.Application.ProductCategories.Commands.UpdateProductCategory;

[Authorize]
public record UpdateProductCategoryCommand : IRequest<Result>
{
    [JsonIgnore]
    public Guid Guid { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
}

public class UpdateProductCategoryCommandHandler : IRequestHandler<UpdateProductCategoryCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductCategoryRepository _productCategoryRepository;

    public UpdateProductCategoryCommandHandler(IUnitOfWork unitOfWork, IProductCategoryRepository productCategoryRepository)
    {
        _unitOfWork = unitOfWork;
        _productCategoryRepository = productCategoryRepository;
    }

    public async Task<Result> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        ProductCategory? productCategory = await _productCategoryRepository.GetByGuidAsync(request.Guid);
        if (productCategory is null) return Result.Fail(DomainErrors.NotFound(nameof(ProductCategory), request.Guid));

        productCategory.Update(request.Name, request.Description);

        _productCategoryRepository.Update(productCategory);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}