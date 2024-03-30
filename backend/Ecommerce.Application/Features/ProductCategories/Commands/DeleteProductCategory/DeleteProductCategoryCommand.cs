namespace Ecommerce.Application.Features.ProductCategories.Commands.DeleteProductCategory;

[Authorize]
public record DeleteProductCategoryCommand(Guid Guid) : IRequest<Result>;

public class DeleteProductCategoryCommandHandler(
    IUnitOfWork unitOfWork, 
    IProductCategoryRepository productCategoryRepository
    ) : IRequestHandler<DeleteProductCategoryCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;

    public async Task<Result> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
    {
        ProductCategory? productCategory = await _productCategoryRepository.GetByIdAsync(request.Guid);

        if (productCategory is null) return Result.Fail(DomainErrors.NotFound(nameof(ProductCategory), request.Guid));

        // TODO: Should not delete if category has active products

        _productCategoryRepository.Delete(productCategory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
