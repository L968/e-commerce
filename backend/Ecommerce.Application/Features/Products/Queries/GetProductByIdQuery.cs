using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<GetProductDto?>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductDto?>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<GetProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(request.Id);
        return _mapper.Map<GetProductDto?>(product);
    }
}
