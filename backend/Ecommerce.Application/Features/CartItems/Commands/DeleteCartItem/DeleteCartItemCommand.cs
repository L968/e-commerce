namespace Ecommerce.Application.Features.CartItems.Commands.DeleteCartItem;

[Authorize]
public record DeleteCartItemCommand(int Id) : IRequest<Result>;

public class DeleteCartItemCommandHandler : IRequestHandler<DeleteCartItemCommand, Result>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCartItemCommandHandler(ICartRepository cartRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await _cartRepository.GetByUserIdAsync(_currentUserService.UserId);

        if (cart is null) return Result.Fail(DomainErrors.Cart.CartNotFound);

        cart.RemoveCartItem(request.Id);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}