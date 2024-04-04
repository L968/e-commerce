using Ecommerce.Application.DTOs.Addresses;

namespace Ecommerce.Application.Features.Addresses.Queries;

[Authorize]
public record GetAddressByIdAndUserIdQuery(Guid Id) : IRequest<GetAddressDto?>;

public class GetAddressByIdAndUserIdQueryHandler(
    IMapper mapper, 
    IAddressRepository addressRepository, 
    ICurrentUserService currentUserService
    ) : IRequestHandler<GetAddressByIdAndUserIdQuery, GetAddressDto?>
{
    private readonly IMapper _mapper = mapper;
    private readonly IAddressRepository _addressRepository = addressRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<GetAddressDto?> Handle(GetAddressByIdAndUserIdQuery request, CancellationToken cancellationToken)
    {
        Address? address = await _addressRepository.GetByIdAndUserIdAsync(request.Id, _currentUserService.UserId);
        return _mapper.Map<GetAddressDto>(address);
    }
}
