using Ecommerce.Application.DTOs.Variants;

namespace Ecommerce.Application.Features.ProductCategories.Queries;

public record GetVariantsByProductCategoryIdQuery(Guid Id) : IRequest<Result<IEnumerable<GetVariantDto>>>;

public class GetVariantsByProductCategoryIdQueryHandler : IRequestHandler<GetVariantsByProductCategoryIdQuery, Result<IEnumerable<GetVariantDto>>>
{
    private readonly IMapper _mapper;
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly IProductCategoryVariantRepository _productCategoryVariantRepository;

    public GetVariantsByProductCategoryIdQueryHandler(IMapper mapper, IProductCategoryRepository productCategoryRepository, IProductCategoryVariantRepository productCategoryVariantRepository)
    {
        _mapper = mapper;
        _productCategoryRepository = productCategoryRepository;
        _productCategoryVariantRepository = productCategoryVariantRepository;
    }

    public async Task<Result<IEnumerable<GetVariantDto>>> Handle(GetVariantsByProductCategoryIdQuery request, CancellationToken cancellationToken)
    {
        ProductCategory? productCategory = await _productCategoryRepository.GetByIdAsync(request.Id);
        if (productCategory is null) return Result.Fail(DomainErrors.NotFound(nameof(ProductCategory), request.Id));

        var productCategoryVariants = await _productCategoryVariantRepository.GetByProductCategoryIdAsync(productCategory.Id);

        var dto = _mapper.Map<IEnumerable<GetVariantDto>>(productCategoryVariants.Select(x => x.Variant));
        return Result.Ok(dto);
    }
}
