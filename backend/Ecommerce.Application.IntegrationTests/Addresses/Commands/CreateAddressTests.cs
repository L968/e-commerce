using Ecommerce.Application.Features.Addresses.Commands.CreateAddress;
using Ecommerce.Application.Features.Addresses.Queries;

namespace Ecommerce.Application.IntegrationTests.Addresses.Commands;

using static Testing;

public class CreateAddressTests : BaseTestFixture
{
    [Test]
    public async Task ShouldInsertAddress_GivenValidData()
    {
        // Arrange
        RunAsRegularUser();

        var command = new CreateAddressCommand()
        {
            RecipientFullName = "John Smith",
            RecipientPhoneNumber = "1234567890",
            PostalCode = "AB123CD",
            StreetName = "Main Street",
            BuildingNumber = "123",
            Complement = "Apt 4B",
            Neighborhood = "Central City",
            City = "Metropolis",
            State = "California",
            Country = "United States",
            AdditionalInformation = "Please deliver to the front desk"
        };

        // Act
        Result<GetAddressDto> result = await SendAsync(command);

        // Assert
        GetAddressDto createdAddress = result.Value;
        Assert.True(result.IsSuccess);
        Assert.IsNotNull(createdAddress);
        Assert.True(createdAddress.Id > 0);

        var query = new GetAddressByIdAndUserIdQuery(createdAddress.Id!.Value);
        GetAddressDto? address = await SendAsync(query);

        Assert.IsNotNull(address);
        Assert.True(address!.Id > 0);
        Assert.AreEqual(address.RecipientFullName, command.RecipientFullName);
        Assert.AreEqual(address.RecipientPhoneNumber, command.RecipientPhoneNumber);
        Assert.AreEqual(address.PostalCode, command.PostalCode);
        Assert.AreEqual(address.StreetName, command.StreetName);
        Assert.AreEqual(address.BuildingNumber, command.BuildingNumber);
        Assert.AreEqual(address.Complement, command.Complement);
        Assert.AreEqual(address.Neighborhood, command.Neighborhood);
        Assert.AreEqual(address.City, command.City);
        Assert.AreEqual(address.State, command.State);
        Assert.AreEqual(address.Country, command.Country);
        Assert.AreEqual(address.AdditionalInformation, command.AdditionalInformation);
    }
}
