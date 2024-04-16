using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.ProductCombinations.Queries;

public record GetProductCombinationByIdQuery(Guid Id) : IRequest<GetProductCombinationByIdDto?>;

public class GetProductCombinationByIdQueryHandler(
    IMapper mapper,
    IProductCombinationRepository productCombinationRepository
    ) : IRequestHandler<GetProductCombinationByIdQuery, GetProductCombinationByIdDto?>
{
    private readonly IMapper _mapper = mapper;
    private readonly IProductCombinationRepository _productCombinationRepository = productCombinationRepository;

    public async Task<GetProductCombinationByIdDto?> Handle(GetProductCombinationByIdQuery request, CancellationToken cancellationToken)
    {
        ProductCombination? productCombination = await _productCombinationRepository.GetByIdAsync(request.Id);
        return _mapper.Map<GetProductCombinationByIdDto?>(productCombination);
    }
}
