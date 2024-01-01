﻿using Ecommerce.Application.DTOs.Carts;

namespace Ecommerce.Application.Features.Carts.Queries;

[Authorize]
public record GetCartByUserIdQuery() : IRequest<IEnumerable<GetCartItemDto>?>;

public class GetCartByUserIdQueryHandler : IRequestHandler<GetCartByUserIdQuery, IEnumerable<GetCartItemDto>?>
{
    private readonly IMapper _mapper;
    private readonly ICartRepository _cartRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetCartByUserIdQueryHandler(IMapper mapper, ICartRepository cartRepository, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _cartRepository = cartRepository;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<GetCartItemDto>?> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
    {
        Cart? cart = await _cartRepository.GetByUserIdAsync(_currentUserService.UserId);
        return _mapper.Map<IEnumerable<GetCartItemDto>>(cart.CartItems);
    }
}
