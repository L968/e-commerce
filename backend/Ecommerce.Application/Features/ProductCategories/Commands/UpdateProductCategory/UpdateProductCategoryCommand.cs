namespace Ecommerce.Application.Features.ProductCategories.Commands.UpdateProductCategory;

[Authorize]
public record UpdateProductCategoryCommand : IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public List<Guid> VariantIds { get; set; } = [];
}

public class UpdateProductCategoryCommandHandler(IUnitOfWork unitOfWork, IProductCategoryRepository productCategoryRepository) : IRequestHandler<UpdateProductCategoryCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;

    public async Task<Result> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        ProductCategory? productCategory = await _productCategoryRepository.GetByIdAsync(request.Id);
        if (productCategory is null) return DomainErrors.NotFound(nameof(ProductCategory), request.Id);

        var result = productCategory.Update(request.Name, request.Description, request.VariantIds);
        if (result.IsFailed) return result;

        _productCategoryRepository.Update(productCategory);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
