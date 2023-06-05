namespace Ecommerce.Application.DTO.AddressDto;

public class GetAddressDto
{
    public int? Id { get; set; }
    public string? RecipientFullName { get; set; }
    public string? RecipientPhoneNumber { get; set; }
    public string? PostalCode { get; set; }
    public string? StreetName { get; set; }
    public string? BuildingNumber { get; set; }
    public string? Complement { get; set; }
    public string? Neighborhood { get; set; }
    public int? CityId { get; set; }
    public City? City { get; set; }
    public string? AdditionalInformation { get; set; }
}