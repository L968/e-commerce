using System.Text.RegularExpressions;

namespace Ecommerce.Domain.Entities.AddressEntities;

public sealed class Address : BaseEntity
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string RecipientFullName { get; private set; }
    public string RecipientPhoneNumber { get; private set; }
    public string PostalCode { get; private set; }
    public string StreetName { get; private set; }
    public string BuildingNumber { get; private set; }
    public string? Complement { get; private set; }
    public string? Neighborhood { get; private set; }
    public int CityId { get; private set; }
    public string? AdditionalInformation { get; private set; }

    public City? City { get; set; }

    public Address(
        int userId,
        string recipientFullName,
        string recipientPhoneNumber,
        string postalCode,
        string streetName,
        string buildingNumber,
        string? complement,
        string? neighborhood,
        int cityId,
        string? additionalInformation
    ) {
        ValidateUserId(userId);
        ValidateDomain(recipientFullName, recipientPhoneNumber, postalCode, streetName, buildingNumber, complement, neighborhood, cityId, additionalInformation);

        UserId = userId;
        RecipientFullName = recipientFullName;
        RecipientPhoneNumber = recipientPhoneNumber;
        PostalCode = postalCode;
        StreetName = streetName;
        BuildingNumber = buildingNumber;
        Complement = complement;
        Neighborhood = neighborhood;
        CityId = cityId;
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
        int cityId,
        string? additionalInformation
    ) {
        ValidateId(id);
        ValidateUserId(userId);
        ValidateDomain(recipientFullName, recipientPhoneNumber, postalCode, streetName, buildingNumber, complement, neighborhood, cityId, additionalInformation);

        Id = id;
        UserId = userId;
        RecipientFullName = recipientFullName;
        RecipientPhoneNumber = recipientPhoneNumber;
        PostalCode = postalCode;
        StreetName = streetName;
        BuildingNumber = buildingNumber;
        Complement = complement;
        Neighborhood = neighborhood;
        CityId = cityId;
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
        int cityId,
        string? additionalInformation
    ) {
        ValidateDomain(recipientFullName, recipientPhoneNumber, postalCode, streetName, buildingNumber, complement, neighborhood, cityId, additionalInformation);

        RecipientFullName = recipientFullName;
        RecipientPhoneNumber = recipientPhoneNumber;
        PostalCode = postalCode;
        StreetName = streetName;
        BuildingNumber = buildingNumber;
        Complement = complement;
        Neighborhood = neighborhood;
        CityId = cityId;
        AdditionalInformation = additionalInformation;
    }

    private void ValidateDomain(
        string recipientFullName,
        string recipientPhoneNumber,
        string postalCode,
        string streetName,
        string buildingNumber,
        string? complement,
        string? neighborhood,
        int cityId,
        string? additionalInformation
    ) {
        ValidateRecipientFullName(recipientFullName);
        ValidateRecipientPhoneNumber(recipientPhoneNumber);
        ValidatePostalCode(postalCode);
        ValidateStreetName(streetName);
        ValidateBuildingNumber(buildingNumber);
        ValidateComplement(complement);
        ValidateNeighborhood(neighborhood);
        ValidateCityId(cityId);
        ValidateAdditionalInformation(additionalInformation);
    }

    private void ValidateId(int id)
    {
        DomainExceptionValidation.When(id <= 0,
            $"Invalid {nameof(id)} value");
    }

    private void ValidateUserId(int userId)
    {
        DomainExceptionValidation.When(userId <= 0,
            $"Invalid {nameof(userId)} value");
    }

    private void ValidateRecipientFullName(string recipientFullName)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(recipientFullName),
            $"Invalid {nameof(recipientFullName)}. It cannot be null or empty");

        DomainExceptionValidation.When(recipientFullName.Length < 3,
            $"Invalid {nameof(recipientFullName)}. It must have at least 3 characters");
    }

    private void ValidateRecipientPhoneNumber(string recipientPhoneNumber)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(recipientPhoneNumber),
            $"Invalid {nameof(recipientPhoneNumber)}. It cannot be null or empty");

        DomainExceptionValidation.When(
            recipientPhoneNumber.Length < 8 ||
            recipientPhoneNumber.Length > 15,
            $"Invalid {nameof(recipientPhoneNumber)}. It must be between 8 and 15 digits long");

        DomainExceptionValidation.When(!Regex.IsMatch(recipientPhoneNumber, @"^[0-9]+$"),
            $"Invalid {nameof(recipientPhoneNumber)}. It must contains only numbers");
    }

    private void ValidatePostalCode(string postalCode)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(postalCode),
            $"Invalid {nameof(postalCode)}. It cannot be null or empty");

        DomainExceptionValidation.When(
            postalCode.Length < 5 ||
            postalCode.Length > 9,
            $"Invalid {nameof(postalCode)}. It must be between 5 and 9 digits long");
    }

    private void ValidateStreetName(string streetName)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(streetName),
            $"Invalid {nameof(streetName)}. It cannot be null or empty");

        DomainExceptionValidation.When(
            streetName.Length < 3 ||
            streetName.Length > 100,
            $"Invalid {nameof(streetName)}. It must have between 3 and 100 characters"
        );
    }

    private void ValidateBuildingNumber(string buildingNumber)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(buildingNumber),
            $"Invalid {nameof(buildingNumber)}. It cannot be null or empty");
    }

    private void ValidateComplement(string? complement)
    {
        DomainExceptionValidation.When(complement?.Length > 100,
            $"Invalid {nameof(complement)}. It cannot exceed 100 characters.");
    }

    private void ValidateNeighborhood(string? neighborhood)
    {
        DomainExceptionValidation.When(neighborhood?.Length > 100,
            $"Invalid {nameof(neighborhood)}. It cannot exceed 100 characters");
    }

    private void ValidateCityId(int cityId)
    {
        DomainExceptionValidation.When(cityId <= 0,
            $"Invalid {nameof(cityId)} value");
    }

    private void ValidateAdditionalInformation(string? additionalInformation)
    {
        DomainExceptionValidation.When(additionalInformation?.Length > 300,
            $"Invalid {nameof(additionalInformation)}. It cannot exceed 100 characters");
    }
}