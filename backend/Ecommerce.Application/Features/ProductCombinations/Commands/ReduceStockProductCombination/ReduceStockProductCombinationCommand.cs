using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.ProductCombinations.Commands.AddProductCombination;

[Authorize]
public record ReduceStockProductCombinationCommand : IRequest
{
    public IReadOnlyList<ReduceStockRequest> Requests { get; init; } = null!;
}

public class ReduceStockProductCombinationCommandHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IProductCombinationRepository productCombinationRepository
    ) : IRequestHandler<ReduceStockProductCombinationCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductCombinationRepository _productCombinationRepository = productCombinationRepository;

    public async Task Handle(ReduceStockProductCombinationCommand request, CancellationToken cancellationToken)
    {
        foreach (var reduceStockRequest in request.Requests)
        {
            ProductCombination? productCombination = await _productCombinationRepository.GetByIdAsync(reduceStockRequest.ProductCombinationId);
            DomainException.ThrowIfNull(productCombination, reduceStockRequest.ProductCombinationId);

            productCombination.Inventory.ReduceStock(reduceStockRequest.Quantity);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
