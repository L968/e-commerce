namespace Ecommerce.Application.Features.CartItems.Commands.DeleteCartItem;

[Authorize]
public record DeleteCartItemCommand(int Id) : IRequest;

public class DeleteCartItemCommandHandler(
    IUnitOfWork unitOfWork,
    ICartRepository cartRepository,
    ICurrentUserService currentUserService
    ) : IRequestHandler<DeleteCartItemCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICartRepository _cartRepository = cartRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await _cartRepository.GetByUserIdAsync(_currentUserService.UserId);
        DomainException.ThrowIfNull(cart, DomainErrors.Cart.CartNotFound, _currentUserService.UserId);

        cart.RemoveCartItem(request.Id);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
