using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.Products.Queries;

public record GetProductQuery(int Page, int PageSize) : IRequest<Pagination<GetProductListDto>>;

public class GetProductQueryHandler(
    IMapper mapper, 
    IProductRepository productRepository
    ) : IRequestHandler<GetProductQuery, Pagination<GetProductListDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Pagination<GetProductListDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var (products, totalItems) = await _productRepository.GetAllAsync(request.Page, request.PageSize);

        return new Pagination<GetProductListDto>(
            request.Page,
            request.PageSize,
            totalItems,
            _mapper.Map<IEnumerable<GetProductListDto>>(products)
        );
    }
}
