namespace Ecommerce.Application.Features.Carts.Commands;

[Authorize]
public record ClearCartItemsCommand(Guid[] ProductCombinationIds) : IRequest;

public class ClearCartItemsCommandHandler(
    IUnitOfWork unitOfWork,
    ICartRepository cartRepository,
    ICurrentUserService currentUserService
    ) : IRequestHandler<ClearCartItemsCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICartRepository _cartRepository = cartRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task Handle(ClearCartItemsCommand request, CancellationToken cancellationToken)
    {
        int userId = _currentUserService.UserId;

        Cart? cart = await _cartRepository.GetByUserIdAsync(userId);
        DomainException.ThrowIfNull(cart, userId);

        cart.ClearItems(request.ProductCombinationIds);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
