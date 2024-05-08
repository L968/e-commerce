namespace Ecommerce.Application.DTOs.Addresses;

public class GetAddressDto
{
    public Guid Id { get; init; }
    public string RecipientFullName { get; init; } = "";
    public string RecipientPhoneNumber { get; init; } = "";
    public string PostalCode { get; init; } = "";
    public string StreetName { get; init; } = "";
    public string BuildingNumber { get; init; } = "";
    public string? Complement { get; init; }
    public string? Neighborhood { get; init; }
    public string City { get; init; } = "";
    public string State { get; init; } = "";
    public string Country { get; init; } = "";
    public string? AdditionalInformation { get; init; }
}
