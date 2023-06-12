using Ecommerce.Application.Addresses.Queries;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Addresses.Commands.CreateAddress;

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

public class AddressCreateCommandHandler : IRequestHandler<CreateAddressCommand, GetAddressDto>
{
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly IAddressRepository _addressRepository;

    public AddressCreateCommandHandler(IMapper mapper, ICurrentUserService currentUserService, IAddressRepository addressRepository)
    {
        _mapper = mapper;
        _addressRepository = addressRepository;
        _currentUserService = currentUserService;
    }

    public async Task<GetAddressDto> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        // TODO: API Cep?

        var address = new Address(
            userId: _currentUserService.UserId!.Value,
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

        await _addressRepository.CreateAsync(address);

        return _mapper.Map<GetAddressDto>(address);
    }
}