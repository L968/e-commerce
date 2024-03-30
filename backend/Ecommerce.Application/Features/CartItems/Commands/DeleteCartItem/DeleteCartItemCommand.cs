namespace Ecommerce.Application.Features.CartItems.Commands.DeleteCartItem;

[Authorize]
public record DeleteCartItemCommand(int Id) : IRequest<Result>;

public class DeleteCartItemCommandHandler(
    IUnitOfWork unitOfWork,
    ICartRepository cartRepository, 
    ICurrentUserService currentUserService
    ) : IRequestHandler<DeleteCartItemCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICartRepository _cartRepository = cartRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<Result> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await _cartRepository.GetByUserIdAsync(_currentUserService.UserId);

        if (cart is null) return Result.Fail(DomainErrors.Cart.CartNotFound);

        cart.RemoveCartItem(request.Id);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
