using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.ProductCombinations.Commands.AddProductCombination;

[Authorize]
public record ReduceStockProductCombinationCommand : IRequest<Result>
{
    public IReadOnlyList<ReduceStockRequest> Requests { get; init; } = null!;
}

public class ReduceStockProductCombinationCommandHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IProductCombinationRepository productCombinationRepository
    ) : IRequestHandler<ReduceStockProductCombinationCommand, Result>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductCombinationRepository _productCombinationRepository = productCombinationRepository;

    public async Task<Result> Handle(ReduceStockProductCombinationCommand request, CancellationToken cancellationToken)
    {
        foreach (var reduceStockRequest in request.Requests)
        {
            ProductCombination? productCombination = await _productCombinationRepository.GetByIdAsync(reduceStockRequest.ProductCombinationId);
            if (productCombination is null) return Result.Fail(DomainErrors.NotFound(nameof(ProductCombination), reduceStockRequest.ProductCombinationId));

            var result = productCombination.Inventory.ReduceStock(reduceStockRequest.Quantity);

            if (result.IsFailed)
                return result;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
