using Ecommerce.Application.DTOs.Variants;

namespace Ecommerce.Application.Features.ProductCategories.Queries;

public record GetVariantsByProductCategoryIdQuery(Guid Id) : IRequest<IEnumerable<GetVariantDto>>;

public class GetVariantsByProductCategoryIdQueryHandler(
    IMapper mapper,
    IProductCategoryRepository productCategoryRepository,
    IProductCategoryVariantRepository productCategoryVariantRepository
    ) : IRequestHandler<GetVariantsByProductCategoryIdQuery, IEnumerable<GetVariantDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;
    private readonly IProductCategoryVariantRepository _productCategoryVariantRepository = productCategoryVariantRepository;

    public async Task<IEnumerable<GetVariantDto>> Handle(GetVariantsByProductCategoryIdQuery request, CancellationToken cancellationToken)
    {
        ProductCategory? productCategory = await _productCategoryRepository.GetByIdAsync(request.Id);
        DomainException.ThrowIfNull(productCategory, request.Id);

        var productCategoryVariants = await _productCategoryVariantRepository.GetByProductCategoryIdAsync(productCategory.Id);

        return _mapper.Map<IEnumerable<GetVariantDto>>(productCategoryVariants.Select(x => x.Variant));
    }
}
