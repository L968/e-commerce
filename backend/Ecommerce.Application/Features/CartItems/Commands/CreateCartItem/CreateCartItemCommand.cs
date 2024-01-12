namespace Ecommerce.Application.Features.CartItems.Commands.CreateCartItem;

[Authorize]
public record CreateCartItemCommand : IRequest<Result>
{
    public int Quantity { get; set; }
    public Guid ProductCombinationId { get; set; }
}

public class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly ICartRepository _cartRepository;
    private readonly IProductCombinationRepository _productCombinationRepository;

    public CreateCartItemCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, ICartRepository cartRepository, IProductCombinationRepository productCombinationRepository)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _cartRepository = cartRepository;
        _productCombinationRepository = productCombinationRepository;
    }

    public async Task<Result> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await _cartRepository.GetByUserIdAsync(_currentUserService.UserId);

        if (cart is null)
        {
            cart = new Cart(_currentUserService.UserId);
            _cartRepository.Create(cart);
        }

        ProductCombination? productCombination = await _productCombinationRepository.GetByIdAsync(request.ProductCombinationId);

        if (productCombination is null)
            return DomainErrors.NotFound(nameof(ProductCombination), request.ProductCombinationId);

        Result<CartItem> createResult = CartItem.Create(cart.Id, request.ProductCombinationId, request.Quantity, true);
        if (createResult.IsFailed) return Result.Fail(createResult.Errors);

        CartItem cartItem = createResult.Value;

        Result addCartItemResult = cart.AddCartItem(cartItem);
        if (addCartItemResult.IsFailed) return addCartItemResult;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
