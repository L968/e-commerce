namespace Ecommerce.Application.Features.Products.Commands.DeleteProduct;

[Authorize]
public record DeleteProductCommand(Guid Id) : IRequest<Result>;

public class DeleteProductCommandHandler(
    IUnitOfWork unitOfWork, 
    IProductRepository productRepository
    ) : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        // TODO: Block delete if product has orders
        Product? product = await _productRepository.GetByIdAsync(request.Id);

        if (product is null) return Result.Fail(DomainErrors.NotFound(nameof(Product), request.Id));

        _productRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
