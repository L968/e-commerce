using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.Products.Queries;

public record GetProductQuery() : IRequest<IEnumerable<GetProductListDto>>;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IEnumerable<GetProductListDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductQueryHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<GetProductListDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetProductListDto>>(products);
    }
}
