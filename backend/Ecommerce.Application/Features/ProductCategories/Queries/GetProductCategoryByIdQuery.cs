using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.ProductCategories.Queries;

public record GetProductCategoryByIdQuery(Guid Id) : IRequest<GetProductCategoryDto?>;

public class GetProductCategoryByIdQueryHandler : IRequestHandler<GetProductCategoryByIdQuery, GetProductCategoryDto?>
{
    private readonly IMapper _mapper;
    private readonly IProductCategoryRepository _productCategoryRepository;

    public GetProductCategoryByIdQueryHandler(IMapper mapper, IProductCategoryRepository productCategoryRepository)
    {
        _mapper = mapper;
        _productCategoryRepository = productCategoryRepository;
    }

    public async Task<GetProductCategoryDto?> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        ProductCategory? productCategory = await _productCategoryRepository.GetByIdAsync(request.Id);
        return _mapper.Map<GetProductCategoryDto?>(productCategory);
    }
}
