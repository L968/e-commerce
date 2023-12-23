using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.Products.Queries;

[Authorize]
public record GetProductDraftsQuery() : IRequest<IEnumerable<GetProductListDto>>;

public class GetProductDraftsQueryHandler : IRequestHandler<GetProductDraftsQuery, IEnumerable<GetProductListDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductDraftsQueryHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<GetProductListDto>> Handle(GetProductDraftsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetDraftsAsync();
        return _mapper.Map<IEnumerable<GetProductListDto>>(products);
    }
}
