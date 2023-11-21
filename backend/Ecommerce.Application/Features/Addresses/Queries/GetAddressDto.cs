namespace Ecommerce.Application.Features.Addresses.Queries;

public record GetAddressDto
{
    public int? Id { get; set; }
    public string? RecipientFullName { get; set; }
    public string? RecipientPhoneNumber { get; set; }
    public string? PostalCode { get; set; }
    public string? StreetName { get; set; }
    public string? BuildingNumber { get; set; }
    public string? Complement { get; set; }
    public string? Neighborhood { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? AdditionalInformation { get; set; }
}
