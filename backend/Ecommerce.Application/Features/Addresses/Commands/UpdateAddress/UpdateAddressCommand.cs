namespace Ecommerce.Application.Features.Addresses.Commands.UpdateAddress;

[Authorize]
public record UpdateAddressCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
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

public class UpdateAddressCommandHandler(
    IUnitOfWork unitOfWork,
    IAddressRepository addressRepository,
    ICurrentUserService currentUserService
    ) : IRequestHandler<UpdateAddressCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAddressRepository _addressRepository = addressRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        Address? address = await _addressRepository.GetByIdAndUserIdAsync(request.Id, _currentUserService.UserId);
        DomainException.ThrowIfNull(address, request.Id);

        address.Update(
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

        _addressRepository.Update(address);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
