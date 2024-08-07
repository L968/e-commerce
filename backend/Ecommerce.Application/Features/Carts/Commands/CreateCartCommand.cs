﻿using Ecommerce.Application.DTOs.Carts;

namespace Ecommerce.Application.Features.Carts.Commands;

[Authorize]
public record CreateCartCommand : IRequest<GetCartDto>;

public class CreateCartCommandHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICartRepository cartRepository,
    ICurrentUserService currentUserService
    ) : IRequestHandler<CreateCartCommand, GetCartDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICartRepository _cartRepository = cartRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<GetCartDto> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        int userId = _currentUserService.UserId;

        Cart? existingCart = await _cartRepository.GetByUserIdAsync(userId);

        if (existingCart is not null)
            throw new DomainException(DomainErrors.Cart.CartAlreadyExists);

        var cart = new Cart(userId);

        _cartRepository.Create(cart);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GetCartDto>(cart);
    }
}
