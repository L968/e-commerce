using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.ProductDiscounts.Queries;

[Authorize]
public record GetProductDiscountsQuery() : IRequest<IEnumerable<GetProductDiscountDto>>;

public class GetProductDiscountsQueryHandler(
    IMapper mapper,
    IProductDiscountRepository productDiscountRepository
    ) : IRequestHandler<GetProductDiscountsQuery, IEnumerable<GetProductDiscountDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IProductDiscountRepository _productDiscountRepository = productDiscountRepository;

    public async Task<IEnumerable<GetProductDiscountDto>> Handle(GetProductDiscountsQuery request, CancellationToken cancellationToken)
    {
        var productDiscount = await _productDiscountRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetProductDiscountDto>>(productDiscount);
    }
}