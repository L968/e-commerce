namespace Ecommerce.Application.Addresses.Commands.UpdateAddress;

[Authorize]
public record UpdateAddressCommand : IRequest<Result>
{
    [JsonIgnore]
    public int Id { get; set; }
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

public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, Result>
{
    private readonly IAddressRepository _addressRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAddressCommandHandler(IAddressRepository addressRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
    {
        _addressRepository = addressRepository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        Address? address = await _addressRepository.GetByIdAndUserIdAsync(request.Id, _currentUserService.UserId);

        if (address is null)
        {
            return Result.Fail("Address not found");
        }

        // TODO: API Cep?

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

        return Result.Ok();
    }
}