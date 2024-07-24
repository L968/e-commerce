using System.Text.RegularExpressions;

namespace Ecommerce.Domain.Entities;

public sealed class Address : AuditableEntity
{
    public Guid Id { get; private set; }
    public int UserId { get; private set; }
    public string RecipientFullName { get; private set; }
    public string RecipientPhoneNumber { get; private set; }
    public string PostalCode { get; private set; }
    public string StreetName { get; private set; }
    public string BuildingNumber { get; private set; }
    public string? Complement { get; private set; }
    public string? Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string? AdditionalInformation { get; private set; }

    private Address() { }

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
        ValidateDomain(recipientPhoneNumber);

        Id = Guid.NewGuid();
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
        ValidateDomain(recipientPhoneNumber);

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

    private static void ValidateDomain(string recipientPhoneNumber)
    {
        var errors = new List<string>();

        if (!Regex.IsMatch(recipientPhoneNumber, @"^[0-9]+$"))
        {
            errors.Add(DomainErrors.Address.InvalidRecipientPhoneNumber);
        }

        if (errors.Count > 0)
            throw new DomainException(errors);
    }
}
