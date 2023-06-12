using Ecommerce.Domain.Interfaces;
using System.Text.Json.Serialization;

namespace Ecommerce.Application.Addresses.Commands.UpdateAddress;

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

public class AddressUpdateCommandHandler : IRequestHandler<UpdateAddressCommand, Result>
{
    private readonly IAddressRepository _addressRepository;
    private readonly ICurrentUserService _currentUserService;

    public AddressUpdateCommandHandler(IAddressRepository addressRepository, ICurrentUserService currentUserService)
    {
        _addressRepository = addressRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        int userId = _currentUserService.UserId!.Value;
        Address? address = await _addressRepository.GetByIdAndUserIdAsync(request.Id, userId);

        if (address == null)
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

        await _addressRepository.UpdateAsync(address);
        return Result.Ok();
    }
}