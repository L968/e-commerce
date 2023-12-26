namespace Ecommerce.Application.Features.ProductCombinations.Commands.DeleteProductCombination;

[Authorize]
public record DeleteProductCombinationCommand(Guid Id) : IRequest<Result>;

public class DeleteProductCombinationCommandHandler : IRequestHandler<DeleteProductCombinationCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductCombinationRepository _productCombinationRepository;

    public DeleteProductCombinationCommandHandler(IUnitOfWork unitOfWork, IProductCombinationRepository productCombinationRepository)
    {
        _unitOfWork = unitOfWork;
        _productCombinationRepository = productCombinationRepository;
    }

    public async Task<Result> Handle(DeleteProductCombinationCommand request, CancellationToken cancellationToken)
    {
        // TODO: Block delete if product has orders
        ProductCombination? productCombination = await _productCombinationRepository.GetByIdAsync(request.Id);

        if (productCombination is null) return Result.Fail(DomainErrors.NotFound(nameof(ProductCombination), request.Id));

        _productCombinationRepository.Delete(productCombination);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
