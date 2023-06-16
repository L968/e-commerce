namespace Ecommerce.Application.CartItems.Commands.CreateCartItem;

[Authorize]
public record CreateCartItemCommand : IRequest<Result>
{
    public int ProductVariantId { get; set; }
    public int Quantity { get; set; }
}

public class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, Result>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICartRepository _cartRepository;
    private readonly IProductVariantRepository _productVariantRepository;

    public CreateCartItemCommandHandler(ICurrentUserService currentUserService, IUnitOfWork unitOfWork, ICartRepository cartRepository, IProductVariantRepository productVariantRepository)
    {
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
        _cartRepository = cartRepository;
        _productVariantRepository = productVariantRepository;
    }

    public async Task<Result> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await _cartRepository.GetByUserIdAsync(_currentUserService.UserId);

        if (cart is null)
        {
            return Result.Fail("Cart not found");
        }

        //ProductVariant? productVariant = await _productVariantRepository.GetByIdAsync(request.ProductVariantId);

        //if (productVariant is null)
        //{
        //    return Result.Fail($"Product variant with ID {request.ProductVariantId} not found");
        //}

        var cartItem = new CartItem(cart.Id, request.ProductVariantId, request.Quantity);
        cart.AddCartItem(cartItem);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}