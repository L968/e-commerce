namespace Ecommerce.Application.DTO.AddressDto;

public record CreateAddressDto
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public string RecipientFullName { get; set; }

    [Required]
    public string RecipientPhoneNumber { get; set; }

    [Required]
    public string PostalCode { get; set; }

    [Required]
    public string StreetName { get; set; }

    [Required]
    public string BuildingNumber { get; set; }

    public string? Complement { get; set; }

    public string? Neighborhood { get; set; }

    [Required]
    public int CityId { get; set; }

    public string? AdditionalInformation { get; set; }
}