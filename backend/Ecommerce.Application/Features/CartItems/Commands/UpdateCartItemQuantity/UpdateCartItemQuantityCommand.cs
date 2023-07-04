namespace Ecommerce.Application.Features.CartItems.Commands.UpdateCartItemQuantity;

[Authorize]
public record UpdateCartItemQuantityCommand : IRequest<Result>
{
    [JsonIgnore]
    public int Id { get; set; }
    public int Quantity { get; set; }
}

public class UpdateCartItemQuantityCommandHandler : IRequestHandler<UpdateCartItemQuantityCommand, Result>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCartItemQuantityCommandHandler(ICartRepository cartRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await _cartRepository.GetByUserIdAsync(_currentUserService.UserId);
        if (cart is null) return Result.Fail(DomainErrors.Cart.CartNotFound);

        CartItem? cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == request.Id);
        if (cartItem is null) return Result.Fail(DomainErrors.NotFound(nameof(CartItem), request.Id));

        cartItem.SetQuantity(request.Quantity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}