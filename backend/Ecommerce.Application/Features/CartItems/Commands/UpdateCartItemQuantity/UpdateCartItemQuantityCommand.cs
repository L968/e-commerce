namespace Ecommerce.Application.Features.CartItems.Commands.UpdateCartItemQuantity;

[Authorize]
public record UpdateCartItemQuantityCommand : IRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public int Quantity { get; set; }
}

public class UpdateCartItemQuantityCommandHandler(
    IUnitOfWork unitOfWork,
    ICartRepository cartRepository,
    ICurrentUserService currentUserService
    ) : IRequestHandler<UpdateCartItemQuantityCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICartRepository _cartRepository = cartRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await _cartRepository.GetByUserIdAsync(_currentUserService.UserId);
        DomainException.ThrowIfNull(cart, DomainErrors.Cart.CartNotFound(_currentUserService.UserId));

        CartItem? cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == request.Id);
        DomainException.ThrowIfNull(cartItem, request.Id);

        cartItem.SetQuantity(request.Quantity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
