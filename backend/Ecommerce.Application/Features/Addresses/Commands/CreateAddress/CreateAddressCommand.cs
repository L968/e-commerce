using Ecommerce.Application.DTOs.Addresses;

namespace Ecommerce.Application.Features.Addresses.Commands.CreateAddress;

[Authorize]
public record CreateAddressCommand : IRequest<Result<GetAddressDto>>
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

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Result<GetAddressDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly IAddressRepository _addressRepository;

    public CreateAddressCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IAddressRepository addressRepository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _addressRepository = addressRepository;
    }

    public async Task<Result<GetAddressDto>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        // TODO: API Cep?
        Result<Address> createResult = Address.Create(
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

        if (createResult.IsFailed) return Result.Fail(createResult.Errors);

        Address address = createResult.Value;
        _addressRepository.Create(address);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<GetAddressDto>(address);
        return Result.Ok(dto);
    }
}