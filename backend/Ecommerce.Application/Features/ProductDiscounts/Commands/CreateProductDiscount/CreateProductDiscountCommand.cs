using Ecommerce.Application.DTOs.Products;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Application.Features.ProductDiscounts.Commands.CreateProductDiscount;

[Authorize]
public record CreateProductDiscountCommand : IRequest<GetProductDiscountDto>
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
    ) : IRequestHandler<CreateProductDiscountCommand, GetProductDiscountDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductDiscountRepository _productDiscountRepository = productDiscountRepository;

    public async Task<GetProductDiscountDto> Handle(CreateProductDiscountCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        DomainException.ThrowIfNull(product, request.ProductId);

        var productDiscounts = await _productDiscountRepository.GetByProductIdAsync(request.ProductId); // TODO: Remove after product entity loads discounts from get method

        bool hasOverlap = ProductDiscount.HasOverlap(request.ValidFrom, request.ValidUntil, productDiscounts);

        if (hasOverlap)
            throw new DomainException(DomainErrors.ProductDiscount.DiscountHasOverlap);

        var productDiscount = new ProductDiscount(
            request.ProductId,
            request.Name,
            request.DiscountValue,
            request.DiscountUnit,
            request.MaximumDiscountAmount,
            request.ValidFrom,
            request.ValidUntil,
            productPrice: 0 // TODO: PASS ALL THE VARIANTS PRICES
        );

        _productDiscountRepository.Create(productDiscount);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GetProductDiscountDto>(productDiscount);
    }
}
