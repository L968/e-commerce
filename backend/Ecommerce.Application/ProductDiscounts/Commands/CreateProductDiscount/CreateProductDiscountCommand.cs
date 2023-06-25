using Ecommerce.Application.ProductDiscounts.Queries;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Application.ProductDiscounts.Commands.CreateProductDiscount;

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

public class CreateProductDiscountCommandHandler : IRequestHandler<CreateProductDiscountCommand, Result<GetProductDiscountDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductDiscountRepository _productDiscountRepository;

    public CreateProductDiscountCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IProductDiscountRepository productDiscountRepository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _productDiscountRepository = productDiscountRepository;
    }

    public async Task<Result<GetProductDiscountDto>> Handle(CreateProductDiscountCommand request, CancellationToken cancellationToken)
    {
        var productDiscounts = await _productDiscountRepository.GetByProductIdAsync(request.ProductId);

        bool hasActiveDiscount = productDiscounts.Any(d => d.IsCurrentlyActive());
        if (hasActiveDiscount) return Result.Fail(DomainErrors.ProductDiscount.DiscountAlreadyExists);

        var createResult = ProductDiscount.Create(
            request.ProductId,
            request.Name,
            request.DiscountValue,
            request.DiscountUnit,
            request.MaximumDiscountAmount,
            request.ValidFrom,
            request.ValidUntil
        );

        if (createResult.IsFailed) return Result.Fail(createResult.Errors);

        var productDiscount = createResult.Value;

        _productDiscountRepository.Create(productDiscount);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<GetProductDiscountDto>(productDiscount);
        return Result.Ok(dto);
    }
}