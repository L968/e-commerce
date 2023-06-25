using System.Text.RegularExpressions;

namespace Ecommerce.Domain.Entities;

public sealed class Address : AuditableEntity
{
    public int? Id { get; private set; }
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

    private Address(
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

    public static Result<Address> Create(
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
        var validationResult = ValidateDomain(recipientPhoneNumber);
        if (validationResult.IsFailed) return validationResult;

        return Result.Ok(new Address(
            userId,
            recipientFullName,
            recipientPhoneNumber,
            postalCode,
            streetName,
            buildingNumber,
            complement,
            neighborhood,
            city,
            state,
            country,
            additionalInformation
        ));
    }

    public Result Update(
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
        var validationResult = ValidateDomain(recipientPhoneNumber);
        if (validationResult.IsFailed) return validationResult;

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
        return Result.Ok();
    }

    private static Result ValidateDomain(string recipientPhoneNumber)
    {
        if (!Regex.IsMatch(recipientPhoneNumber, @"^[0-9]+$"))
        {
            return Result.Fail(DomainErrors.Address.InvalidRecipientPhoneNumber);
        }

        return Result.Ok();
    }
}