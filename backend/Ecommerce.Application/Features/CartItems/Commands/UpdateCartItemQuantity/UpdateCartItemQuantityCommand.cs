namespace Ecommerce.Application.Features.CartItems.Commands.UpdateCartItemQuantity;

[Authorize]
public record UpdateCartItemQuantityCommand : IRequest<Result>
{
    [JsonIgnore]
    public int Id { get; set; }
    public int Quantity { get; set; }
}

public class UpdateCartItemQuantityCommandHandler(
    IUnitOfWork unitOfWork,
    ICartRepository cartRepository,
    ICurrentUserService currentUserService
    ) : IRequestHandler<UpdateCartItemQuantityCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICartRepository _cartRepository = cartRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<Result> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await _cartRepository.GetByUserIdAsync(_currentUserService.UserId);
        if (cart is null) return DomainErrors.Cart.CartNotFound;

        CartItem? cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == request.Id);
        if (cartItem is null) return DomainErrors.NotFound(nameof(CartItem), request.Id);

        cartItem.SetQuantity(request.Quantity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
