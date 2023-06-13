using Ecommerce.Application.Addresses.Commands.CreateAddress;
using Ecommerce.Application.Addresses.Queries;

namespace Ecommerce.Application.IntegrationTests.Addresses.Commands;

using static Testing;

public class UpdateAddressTests : BaseTestFixture
{
    [Test]
    public async Task ShouldUpdateAddress_GivenValidData()
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
        GetAddressDto createdAddress = await SendAsync(command);

        // Assert
        Assert.IsNotNull(createdAddress);
        Assert.True(createdAddress.Id > 0);

        var query = new GetAddressByIdAndUserIdQuery(createdAddress.Id!.Value);
        GetAddressDto? address = await SendAsync(query);

        Assert.IsNotNull(address);
        Assert.True(address!.Id > 0);
    }
}