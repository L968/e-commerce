namespace Ecommerce.Application.Features.ProductCategories.Commands.DeleteProductCategory;

[Authorize]
public record DeleteProductCategoryCommand(Guid Guid) : IRequest;

public class DeleteProductCategoryCommandHandler(
    IUnitOfWork unitOfWork,
    IProductCategoryRepository productCategoryRepository
    ) : IRequestHandler<DeleteProductCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;

    public async Task Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
    {
        ProductCategory? productCategory = await _productCategoryRepository.GetByIdAsync(request.Guid);
        DomainException.ThrowIfNull(productCategory, request.Guid);

        // TODO: Should not delete if category has active products

        _productCategoryRepository.Delete(productCategory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
