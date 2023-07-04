namespace Ecommerce.Application.Features.ProductCategories.Commands.DeleteProductCategory;

[Authorize]
public record DeleteProductCategoryCommand(Guid Guid) : IRequest<Result>;

public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductCategoryRepository _productCategoryRepository;

    public DeleteProductCategoryCommandHandler(IUnitOfWork unitOfWork, IProductCategoryRepository productCategoryRepository)
    {
        _unitOfWork = unitOfWork;
        _productCategoryRepository = productCategoryRepository;
    }

    public async Task<Result> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
    {
        ProductCategory? productCategory = await _productCategoryRepository.GetByGuidAsync(request.Guid);

        if (productCategory is null) return Result.Fail(DomainErrors.NotFound(nameof(ProductCategory), request.Guid));

        // TODO: Should not delete if category has active products

        _productCategoryRepository.Delete(productCategory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}