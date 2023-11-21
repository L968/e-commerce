namespace Ecommerce.Application.Features.CartItems.Commands.CreateCartItem;

[Authorize]
public record CreateCartItemCommand : IRequest<Result>
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly ICartRepository _cartRepository;
    private readonly IProductVariationRepository _productVariantRepository;

    public CreateCartItemCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, ICartRepository cartRepository, IProductVariationRepository productVariantRepository)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _cartRepository = cartRepository;
        _productVariantRepository = productVariantRepository;
    }

    public async Task<Result> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await _cartRepository.GetByUserIdAsync(_currentUserService.UserId);

        if (cart is null) return Result.Fail(DomainErrors.Cart.CartNotFound);

        //ProductVariant? productVariant = await _productVariantRepository.GetByIdAsync(request.ProductVariantId);

        //if (productVariant is null)
        //{
        //    return Result.Fail($"Product variant with ID {request.ProductVariantId} not found");
        //}

        Result<CartItem> createResult = CartItem.Create(cart.Id, request.ProductId, request.Quantity, false);
        if (createResult.IsFailed) return Result.Fail(createResult.Errors);

        CartItem cartItem = createResult.Value;

        Result addCartItemResult = cart.AddCartItem(cartItem);
        if (addCartItemResult.IsFailed) return addCartItemResult;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}