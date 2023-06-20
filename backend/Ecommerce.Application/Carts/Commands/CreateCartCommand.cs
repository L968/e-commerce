using Ecommerce.Application.Carts.Queries;

namespace Ecommerce.Application.Carts.Commands;

[Authorize]
public record CreateCartCommand : IRequest<Result<GetCartDto>>;

public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, Result<GetCartDto>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCartCommandHandler(ICurrentUserService currentUserService, ICartRepository cartRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GetCartDto>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        int userId = _currentUserService.UserId;

        Cart? existingCart = await _cartRepository.GetByUserIdAsync(userId);
        if (existingCart is not null) return Result.Fail(DomainErrors.Cart.CartAlreadyExists);

        var cart = new Cart(userId);

        _cartRepository.Create(cart);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(_mapper.Map<GetCartDto>(cart));
    }
}