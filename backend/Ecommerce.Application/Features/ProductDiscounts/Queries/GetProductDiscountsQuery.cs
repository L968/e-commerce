using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.ProductDiscounts.Queries;

[Authorize]
public record GetProductDiscountsQuery() : IRequest<IEnumerable<GetProductDiscountDto>>;

public class GetProductDiscountsQueryHandler : IRequestHandler<GetProductDiscountsQuery, IEnumerable<GetProductDiscountDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductDiscountRepository _productDiscountRepository;

    public GetProductDiscountsQueryHandler(IMapper mapper, IProductDiscountRepository productDiscountRepository)
    {
        _mapper = mapper;
        _productDiscountRepository = productDiscountRepository;
    }

    public async Task<IEnumerable<GetProductDiscountDto>> Handle(GetProductDiscountsQuery request, CancellationToken cancellationToken)
    {
        var productDiscount = await _productDiscountRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetProductDiscountDto>>(productDiscount);
    }
}