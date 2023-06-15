using Ecommerce.Application.Carts.Queries;
using Ecommerce.Domain.Repositories;

namespace Ecommerce.Application.Carts.Commands;

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
        int? userId = _currentUserService.UserId;

        if (userId is null)
        {
            return Result.Fail("UserId is required");
        }

        Cart? existingCart = await _cartRepository.GetByUserIdAsync(userId.Value);

        if (existingCart is not null)
        {
            return Result.Fail("A cart already exists for the current user");
        }

        var cart = new Cart(userId.Value);

        _cartRepository.Create(cart);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(_mapper.Map<GetCartDto>(cart));
    }
}