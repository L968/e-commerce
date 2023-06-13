using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Addresses.Queries;

public record GetAddressByUserIdQuery : IRequest<IEnumerable<GetAddressDto>>;

public class GetAddressByUserIdQueryHandler : IRequestHandler<GetAddressByUserIdQuery, IEnumerable<GetAddressDto>>
{
    private readonly IMapper _mapper;
    private readonly IAddressRepository _addressRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetAddressByUserIdQueryHandler(IMapper mapper, IAddressRepository addressRepository, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _addressRepository = addressRepository;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<GetAddressDto>> Handle(GetAddressByUserIdQuery request, CancellationToken cancellationToken)
    {
        int userId = _currentUserService.UserId!.Value;
        IEnumerable<Address> addresses = await _addressRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<GetAddressDto>>(addresses);
    }
}