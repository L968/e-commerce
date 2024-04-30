using Ecommerce.Application.DTOs.Products;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Application.Features.ProductDiscounts.Commands.CreateProductDiscount;

[Authorize]
public record CreateProductDiscountCommand : IRequest<Result<GetProductDiscountDto>>
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = "";
    public decimal DiscountValue { get; set; }
    public DiscountUnit DiscountUnit { get; set; }
    public decimal? MaximumDiscountAmount { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidUntil { get; set; }
}

public class CreateProductDiscountCommandHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IProductRepository productRepository,
    IProductDiscountRepository productDiscountRepository
    ) : IRequestHandler<CreateProductDiscountCommand, Result<GetProductDiscountDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductDiscountRepository _productDiscountRepository = productDiscountRepository;

    public async Task<Result<GetProductDiscountDto>> Handle(CreateProductDiscountCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product is null) return DomainErrors.NotFound(nameof(Product), request.ProductId);

        var productDiscounts = await _productDiscountRepository.GetByProductIdAsync(request.ProductId); // TODO: Remove after product entity loads discounts from get method

        bool hasOverlap = ProductDiscount.HasOverlap(request.ValidFrom, request.ValidUntil, productDiscounts);
        if (hasOverlap) return DomainErrors.ProductDiscount.DiscountHasOverlap;

        var createResult = ProductDiscount.Create(
            request.ProductId,
            request.Name,
            request.DiscountValue,
            request.DiscountUnit,
            request.MaximumDiscountAmount,
            request.ValidFrom,
            request.ValidUntil,
            productPrice: 0 // TODO: PASS ALL THE VARIANTS PRICES
        );

        if (createResult.IsFailed) return Result.Fail(createResult.Errors);

        var productDiscount = createResult.Value;

        _productDiscountRepository.Create(productDiscount);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<GetProductDiscountDto>(productDiscount);
        return Result.Ok(dto);
    }
}
