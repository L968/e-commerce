namespace Ecommerce.Application.Products.Commands.DeleteProduct;

[Authorize]
public record RemoveProductImageCommand(Guid Id, int ProductImageId) : IRequest<Result>;

public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;

    public RemoveProductImageCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    public async Task<Result> Handle(RemoveProductImageCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null) return Result.Fail(DomainErrors.NotFound(nameof(Product), request.Id));

        product.RemoveImage(request.ProductImageId);

        // TODO: Remove from upload system

        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}