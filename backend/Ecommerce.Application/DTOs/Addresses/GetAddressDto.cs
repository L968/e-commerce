﻿namespace Ecommerce.Application.DTOs.Addresses;

public class GetAddressDto
{
    public int? Id { get; set; }
    public int UserId { get; set; }
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
