using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Features.ProductCombinations.Commands.DeleteProductCombination;

[Authorize]
public record DeleteProductCombinationCommand(Guid Id) : IRequest<Result>;

public class DeleteProductCombinationCommandHandler(
    IUnitOfWork unitOfWork, 
    IProductRepository productRepository,
    IBlobStorageService blobStorageService,
    IProductCombinationRepository productCombinationRepository
    ) : IRequestHandler<DeleteProductCombinationCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IBlobStorageService _blobStorageService = blobStorageService;
    private readonly IProductCombinationRepository _productCombinationRepository = productCombinationRepository;

    public async Task<Result> Handle(DeleteProductCombinationCommand request, CancellationToken cancellationToken)
    {
        // TODO: Block if product has orders
        ProductCombination? productCombination = await _productCombinationRepository.GetByIdAsync(request.Id);
        if (productCombination is null) return Result.Fail(DomainErrors.NotFound(nameof(ProductCombination), request.Id));

        Product? product = await _productRepository.GetByIdAsync(productCombination.ProductId);
        if (product is null) return Result.Fail(DomainErrors.NotFound(nameof(product), productCombination.ProductId));

        product.RemoveVariantOptionsByCombination(productCombination.Id);

        await _blobStorageService.RemoveImage(productCombination.Images.Select(i => i.ImagePath));

        _productCombinationRepository.Delete(productCombination);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
