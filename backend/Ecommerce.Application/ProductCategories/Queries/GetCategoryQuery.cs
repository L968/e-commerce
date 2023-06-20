namespace Ecommerce.Application.ProductCategories.Queries;

public record GetProductCategoryQuery() : IRequest<IEnumerable<GetProductCategoryDto>>;

public class GetProductCategoryQueryHandler : IRequestHandler<GetProductCategoryQuery, IEnumerable<GetProductCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductCategoryRepository _productCategoryRepository;

    public GetProductCategoryQueryHandler(IMapper mapper, IProductCategoryRepository productCategoryRepository)
    {
        _mapper = mapper;
        _productCategoryRepository = productCategoryRepository;
    }

    public async Task<IEnumerable<GetProductCategoryDto>> Handle(GetProductCategoryQuery request, CancellationToken cancellationToken)
    {
        var productCategory = await _productCategoryRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetProductCategoryDto>>(productCategory);
    }
}