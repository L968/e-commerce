using System.Text.RegularExpressions;

namespace Ecommerce.Domain.Entities;

public sealed class Address : AuditableEntity
{
    public int? Id { get; private set; }
    public int UserId { get; private set; }
    public string RecipientFullName { get; private set; }

    private string recipientPhoneNumber = "";
    public string RecipientPhoneNumber
    {
        get => recipientPhoneNumber;
        private set
        {
            DomainExceptionValidation.When(!Regex.IsMatch(value, @"^[0-9]+$"),
                "RecipientPhoneNumber must contains numbers only");

            recipientPhoneNumber = value;
        }
    }

    public string PostalCode { get; private set; }
    public string StreetName { get; private set; }
    public string BuildingNumber { get; private set; }
    public string? Complement { get; private set; }
    public string? Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string? AdditionalInformation { get; private set; }

    public Address(
        int userId,
        string recipientFullName,
        string recipientPhoneNumber,
        string postalCode,
        string streetName,
        string buildingNumber,
        string? complement,
        string? neighborhood,
        string city,
        string state,
        string country,
        string? additionalInformation
    )
    {
        UserId = userId;
        RecipientFullName = recipientFullName;
        RecipientPhoneNumber = recipientPhoneNumber;
        PostalCode = postalCode;
        StreetName = streetName;
        BuildingNumber = buildingNumber;
        Complement = complement;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        AdditionalInformation = additionalInformation;
    }

    public Address(
        int id,
        int userId,
        string recipientFullName,
        string recipientPhoneNumber,
        string postalCode,
        string streetName,
        string buildingNumber,
        string? complement,
        string? neighborhood,
        string city,
        string state,
        string country,
        string? additionalInformation
    )
    {
        Id = id;
        UserId = userId;
        RecipientFullName = recipientFullName;
        RecipientPhoneNumber = recipientPhoneNumber;
        PostalCode = postalCode;
        StreetName = streetName;
        BuildingNumber = buildingNumber;
        Complement = complement;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        AdditionalInformation = additionalInformation;
    }

    public void Update(
        string recipientFullName,
        string recipientPhoneNumber,
        string postalCode,
        string streetName,
        string buildingNumber,
        string? complement,
        string? neighborhood,
        string city,
        string state,
        string country,
        string? additionalInformation
    )
    {
        RecipientFullName = recipientFullName;
        RecipientPhoneNumber = recipientPhoneNumber;
        PostalCode = postalCode;
        StreetName = streetName;
        BuildingNumber = buildingNumber;
        Complement = complement;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        AdditionalInformation = additionalInformation;
    }
}