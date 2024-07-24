using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.Products.Queries;

[Authorize]
public record GetProductDraftsQuery() : IRequest<IEnumerable<GetProductListDto>>;

public class GetProductDraftsQueryHandler(
    IMapper mapper,
    IProductRepository productRepository
    ) : IRequestHandler<GetProductDraftsQuery, IEnumerable<GetProductListDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<IEnumerable<GetProductListDto>> Handle(GetProductDraftsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetDraftsAsync();
        return _mapper.Map<IEnumerable<GetProductListDto>>(products);
    }
}
