namespace Ecommerce.Application.Features.Products.Commands.RemoveProductImage;

[Authorize]
public record RemoveProductImageCommand(Guid Id, int ProductImageId) : IRequest<Result>;

public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductCombinationRepository _productCombinationRepository;

    public RemoveProductImageCommandHandler(IUnitOfWork unitOfWork, IProductCombinationRepository productCombinationRepository)
    {
        _unitOfWork = unitOfWork;
        _productCombinationRepository = productCombinationRepository;
    }

    public async Task<Result> Handle(RemoveProductImageCommand request, CancellationToken cancellationToken)
    {
        ProductCombination? product = await _productCombinationRepository.GetByIdAsync(request.Id);
        if (product is null) return Result.Fail(DomainErrors.NotFound(nameof(Product), request.Id));

        product.RemoveImage(request.ProductImageId);

        // TODO: Remove from upload system

        _productCombinationRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
