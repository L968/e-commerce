namespace Ecommerce.Application.Features.ProductCombinations.Commands.DeleteProductCombination;

[Authorize]
public record DeleteProductCombinationCommand(Guid Id) : IRequest;

public class DeleteProductCombinationCommandHandler(
    IUnitOfWork unitOfWork,
    IProductRepository productRepository,
    IBlobStorageService blobStorageService,
    IProductCombinationRepository productCombinationRepository
    ) : IRequestHandler<DeleteProductCombinationCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IBlobStorageService _blobStorageService = blobStorageService;
    private readonly IProductCombinationRepository _productCombinationRepository = productCombinationRepository;

    public async Task Handle(DeleteProductCombinationCommand request, CancellationToken cancellationToken)
    {
        // TODO: Block if product has orders
        ProductCombination? productCombination = await _productCombinationRepository.GetByIdAsync(request.Id);
        DomainException.ThrowIfNull(productCombination, request.Id);

        Product? product = await _productRepository.GetByIdAsync(productCombination.ProductId);
        DomainException.ThrowIfNull(product, productCombination.ProductId);

        product.RemoveVariantOptionsByCombination(productCombination.Id);

        await _blobStorageService.RemoveImage(productCombination.Images.Select(i => i.ImagePath));

        _productCombinationRepository.Delete(productCombination);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
