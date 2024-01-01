using Ecommerce.Application.DTOs.Addresses;

namespace Ecommerce.Application.Features.Addresses.Queries;

[Authorize]
public record GetAddressByIdAndUserIdQuery(int Id) : IRequest<GetAddressDto?>;

public class GetAddressByIdAndUserIdQueryHandler : IRequestHandler<GetAddressByIdAndUserIdQuery, GetAddressDto?>
{
    private readonly IMapper _mapper;
    private readonly IAddressRepository _addressRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetAddressByIdAndUserIdQueryHandler(IMapper mapper, IAddressRepository addressRepository, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _addressRepository = addressRepository;
        _currentUserService = currentUserService;
    }

    public async Task<GetAddressDto?> Handle(GetAddressByIdAndUserIdQuery request, CancellationToken cancellationToken)
    {
        Address? address = await _addressRepository.GetByIdAndUserIdAsync(request.Id, _currentUserService.UserId);
        return _mapper.Map<GetAddressDto>(address);
    }
}