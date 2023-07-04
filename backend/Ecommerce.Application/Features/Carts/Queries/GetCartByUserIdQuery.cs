namespace Ecommerce.Application.Features.Carts.Queries;

[Authorize]
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
        Cart? cart = await _cartRepository.GetByUserIdAsync(_currentUserService.UserId);
        return _mapper.Map<GetCartDto>(cart);
    }
}