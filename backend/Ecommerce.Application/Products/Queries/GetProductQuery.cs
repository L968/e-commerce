namespace Ecommerce.Application.Products.Queries;

public record GetProductQuery() : IRequest<IEnumerable<GetProductDto>>;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IEnumerable<GetProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductQueryHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<GetProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetProductDto>>(product);
    }
}