using Ecommerce.Application.DTOs.Addresses;

namespace Ecommerce.Application.Features.Addresses.Queries;

[Authorize]
public record GetAddressByUserIdQuery : IRequest<IEnumerable<GetAddressDto>>;

public class GetAddressByUserIdQueryHandler(
    IMapper mapper, 
    IAddressRepository addressRepository, 
    ICurrentUserService currentUserService
    ) : IRequestHandler<GetAddressByUserIdQuery, IEnumerable<GetAddressDto>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IAddressRepository _addressRepository = addressRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<IEnumerable<GetAddressDto>> Handle(GetAddressByUserIdQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Address> addresses = await _addressRepository.GetByUserIdAsync(_currentUserService.UserId);
        return _mapper.Map<IEnumerable<GetAddressDto>>(addresses);
    }
}