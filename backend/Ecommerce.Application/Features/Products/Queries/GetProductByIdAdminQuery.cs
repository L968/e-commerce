using Ecommerce.Application.DTOs.Products.Admin;

namespace Ecommerce.Application.Features.Products.Queries;

[Authorize]
public record GetProductByIdAdminQuery(Guid Id) : IRequest<GetProductAdminDto?>;

public class GetProductByIdAdminQueryHandler(
    IMapper mapper,
    IProductRepository productRepository
    ) : IRequestHandler<GetProductByIdAdminQuery, GetProductAdminDto?>
{
    private readonly IMapper _mapper = mapper;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<GetProductAdminDto?> Handle(GetProductByIdAdminQuery request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAdminAsync(request.Id);
        return _mapper.Map<GetProductAdminDto?>(product);
    }
}
