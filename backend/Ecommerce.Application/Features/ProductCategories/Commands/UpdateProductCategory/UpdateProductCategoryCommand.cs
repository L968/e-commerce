namespace Ecommerce.Application.Features.ProductCategories.Commands.UpdateProductCategory;

[Authorize]
public record UpdateProductCategoryCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public List<Guid> VariantIds { get; set; } = [];
}

public class UpdateProductCategoryCommandHandler(
    IUnitOfWork unitOfWork,
    IProductCategoryRepository productCategoryRepository
) : IRequestHandler<UpdateProductCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;

    public async Task Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        ProductCategory? productCategory = await _productCategoryRepository.GetByIdAsync(request.Id);
        DomainException.ThrowIfNull(productCategory, request.Id);

        productCategory.Update(request.Name, request.Description, request.VariantIds);

        _productCategoryRepository.Update(productCategory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
