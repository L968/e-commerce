using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.ProductCategories.Queries;

public record GetProductCategoryQuery() : IRequest<IEnumerable<GetProductCategoryDto>>;

public class GetProductCategoryQueryHandler(
    IMapper mapper, 
    IProductCategoryRepository productCategoryRepository
    ) : IRequestHandler<GetProductCategoryQuery, IEnumerable<GetProductCategoryDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;

    public async Task<IEnumerable<GetProductCategoryDto>> Handle(GetProductCategoryQuery request, CancellationToken cancellationToken)
    {
        var productCategory = await _productCategoryRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetProductCategoryDto>>(productCategory);
    }
}
