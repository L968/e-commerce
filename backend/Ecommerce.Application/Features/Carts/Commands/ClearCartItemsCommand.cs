namespace Ecommerce.Application.Features.Carts.Commands;

[Authorize]
public record ClearCartItemsCommand(Guid[] ProductCombinationIds) : IRequest<Result>;

public class ClearCartItemsCommandHandler(
    IUnitOfWork unitOfWork,
    ICartRepository cartRepository,
    ICurrentUserService currentUserService
    ) : IRequestHandler<ClearCartItemsCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICartRepository _cartRepository = cartRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<Result> Handle(ClearCartItemsCommand request, CancellationToken cancellationToken)
    {
        int userId = _currentUserService.UserId;

        Cart? cart = await _cartRepository.GetByUserIdAsync(userId);
        if (cart is null) return DomainErrors.NotFound(nameof(Cart), userId);

        cart.ClearItems(request.ProductCombinationIds);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
