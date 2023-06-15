namespace Ecommerce.Application.Carts.Queries;

public record GetCartByUserIdQuery() : IRequest<GetCartDto?>;

public class GetCartByUserIdQueryHandler : IRequestHandler<GetCartByUserIdQuery, GetCartDto?>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    public GetCartByUserIdQueryHandler(ICurrentUserService currentUserService, ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<GetCartDto?> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
    {
        if (_currentUserService.UserId is null)
        {
            throw new ArgumentNullException(nameof(_currentUserService.UserId));
        }

        Cart? cart = await _cartRepository.GetByUserIdAsync(_currentUserService.UserId.Value);
        return _mapper.Map<GetCartDto>(cart);
    }
}