using Ecommerce.Application.DTOs.Addresses;

namespace Ecommerce.Application.Features.Addresses.Commands.CreateAddress;

[Authorize]
public record CreateAddressCommand : IRequest<GetAddressDto>
{
    public string RecipientFullName { get; set; } = "";
    public string RecipientPhoneNumber { get; set; } = "";
    public string PostalCode { get; set; } = "";
    public string StreetName { get; set; } = "";
    public string BuildingNumber { get; set; } = "";
    public string? Complement { get; set; }
    public string? Neighborhood { get; set; }
    public string City { get; set; } = "";
    public string State { get; set; } = "";
    public string Country { get; set; } = "";
    public string? AdditionalInformation { get; set; }
}

public class CreateAddressCommandHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IAddressRepository addressRepository,
    ICurrentUserService currentUserService,
    IAuthorizationService authorizationService
    ) : IRequestHandler<CreateAddressCommand, GetAddressDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAddressRepository _addressRepository = addressRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IAuthorizationService _authorizationService = authorizationService;

    public async Task<GetAddressDto> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = new Address(
            userId: _currentUserService.UserId,
            request.RecipientFullName,
            request.RecipientPhoneNumber,
            request.PostalCode,
            request.StreetName,
            request.BuildingNumber,
            request.Complement,
            request.Neighborhood,
            request.City,
            request.State,
            request.Country,
            request.AdditionalInformation
        );

        _addressRepository.Create(address);

        var userAddresses = await _addressRepository.GetByUserIdAsync(_currentUserService.UserId);

        if (!userAddresses.Any())
        {
            await _authorizationService.UpdateDefaultAddressIdAsync(address.Id);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GetAddressDto>(address);
    }
}
