namespace Ecommerce.Application.Features.CartItems.Commands.CreateCartItem;

[Authorize]
public record CreateCartItemCommand : IRequest
{
    public int Quantity { get; set; }
    public Guid ProductCombinationId { get; set; }
}

public class CreateCartItemCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    ICartRepository cartRepository,
    IProductCombinationRepository productCombinationRepository
    ) : IRequestHandler<CreateCartItemCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly ICartRepository _cartRepository = cartRepository;
    private readonly IProductCombinationRepository _productCombinationRepository = productCombinationRepository;

    public async Task Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await _cartRepository.GetByUserIdAsync(_currentUserService.UserId);

        if (cart is null)
        {
            cart = new Cart(_currentUserService.UserId);
            _cartRepository.Create(cart);
        }

        ProductCombination? productCombination = await _productCombinationRepository.GetByIdAsync(request.ProductCombinationId);
        DomainException.ThrowIfNull(productCombination, request.ProductCombinationId);

        var cartItem = new CartItem(cart.Id, request.ProductCombinationId, request.Quantity);

        cart.AddCartItem(cartItem);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
