using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.Products.Queries;

public record GetProductByIdAdminQuery(Guid Id) : IRequest<GetProductAdminDto?>;

public class GetProductByIdAdminQueryHandler : IRequestHandler<GetProductByIdAdminQuery, GetProductAdminDto?>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductByIdAdminQueryHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<GetProductAdminDto?> Handle(GetProductByIdAdminQuery request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAdminAsync(request.Id);
        return _mapper.Map<GetProductAdminDto?>(product);
    }
}
