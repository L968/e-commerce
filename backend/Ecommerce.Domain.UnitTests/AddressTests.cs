using Bogus;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.UnitTests;

public  class AddressTests
{
    private readonly Faker<Address> _addressFaker;

    public AddressTests()
    {
        _addressFaker = new Faker<Address>()
            .CustomInstantiator(f => Address.Create(
                userId: f.Random.Number(1, 1000),
                recipientFullName: f.Name.FullName(),
                recipientPhoneNumber: f.Random.Number(900000000, 999999999).ToString(),
                postalCode: f.Address.ZipCode(),
                streetName: f.Address.StreetName(),
                buildingNumber: f.Address.BuildingNumber(),
                complement: f.Random.Number(100, 900).ToString(),
                neighborhood: f.Random.String2(10),
                city: f.Address.City(),
                state: f.Address.State(),
                country: f.Address.Country(),
                additionalInformation: f.Random.String2(10)
             ).Value);
    }

    [Fact]
    public void CreateAddress_WithValidData_ShouldCreateAddress()
    {
        // Arrange
        var addressData = _addressFaker.Generate();
        int userId = 1;

        // Act
        var result = Address.Create(
            userId,
            addressData.RecipientFullName,
            addressData.RecipientPhoneNumber,
            addressData.PostalCode,
            addressData.StreetName,
            addressData.BuildingNumber,
            null,
            null,
            addressData.City,
            addressData.State,
            addressData.Country,
            null
        );

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("999-999")]
    [InlineData("999.999")]
    [InlineData("(99) 999999")]
    public void CreateAddress_WithInvalidRecipientPhoneNumber_ShouldFail(string invalidPhoneNumber)
    {
        // Arrange
        var addressData = _addressFaker.Generate();
        int userId = 1;

        // Act
        var result = Address.Create(
            userId,
            addressData.RecipientFullName,
            invalidPhoneNumber,
            addressData.PostalCode,
            addressData.StreetName,
            addressData.BuildingNumber,
            null,
            null,
            addressData.City,
            addressData.State,
            addressData.Country,
            null
        );

        // Assert
        Assert.True(result.IsFailed);
    }

    [Fact]
    public void UpdateAddress_WithValidData_ShouldUpdateAddress()
    {
        // Arrange
        var address = _addressFaker.Generate();
        var newAddress= _addressFaker.Generate();

        // Act
        var result = address.Update(
            newAddress.RecipientFullName,
            newAddress.RecipientPhoneNumber,
            newAddress.PostalCode,
            newAddress.StreetName,
            newAddress.BuildingNumber,
            newAddress.Complement,
            newAddress.Neighborhood,
            newAddress.City,
            newAddress.State,
            newAddress.Country,
            newAddress.AdditionalInformation
        );

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(newAddress.RecipientFullName, address.RecipientFullName);
        Assert.Equal(newAddress.RecipientPhoneNumber, address.RecipientPhoneNumber);
        Assert.Equal(newAddress.PostalCode, address.PostalCode);
        Assert.Equal(newAddress.StreetName, address.StreetName);
        Assert.Equal(newAddress.BuildingNumber, address.BuildingNumber);
        Assert.Equal(newAddress.Complement, address.Complement);
        Assert.Equal(newAddress.Neighborhood, address.Neighborhood);
        Assert.Equal(newAddress.City, address.City);
        Assert.Equal(newAddress.State, address.State);
        Assert.Equal(newAddress.Country, address.Country);
        Assert.Equal(newAddress.AdditionalInformation, address.AdditionalInformation);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("999-999")]
    [InlineData("999.999")]
    [InlineData("(99) 999999")]
    public void UpdateAddress_WithInvalidRecipientPhoneNumber_ShouldFail(string invalidPhoneNumber)
    {
        // Arrange
        var address = _addressFaker.Generate();
        var newAddress = _addressFaker.Generate();

        // Act
        var result = address.Update(
            newAddress.RecipientFullName,
            invalidPhoneNumber,
            newAddress.PostalCode,
            newAddress.StreetName,
            newAddress.BuildingNumber,
            newAddress.Complement,
            newAddress.Neighborhood,
            newAddress.City,
            newAddress.State,
            newAddress.Country,
            newAddress.AdditionalInformation
        );

        // Assert
        Assert.True(result.IsFailed);
    }
}
