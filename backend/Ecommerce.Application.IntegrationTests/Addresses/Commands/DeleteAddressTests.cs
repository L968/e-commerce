using Ecommerce.Application.Addresses.Commands;
using Ecommerce.Application.Addresses.Commands.CreateAddress;
using Ecommerce.Application.Addresses.Queries;
using FluentResults;

namespace Ecommerce.Application.IntegrationTests.Addresses.Commands;

using static Testing;

public class DeleteAddressTests : BaseTestFixture
{
    [Test]
    public async Task ShouldDeleteAddress_GivenExistingAddress()
    {
        // Arrange
        RunAsRegularUser();

        var createCommand = new CreateAddressCommand()
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

        GetAddressDto createdAddress = await SendAsync(createCommand);
        var deleteCommand = new DeleteAddressCommand(createdAddress.Id!.Value);
        var query = new GetAddressByIdAndUserIdQuery(createdAddress.Id!.Value);

        // Act
        Result result = await SendAsync(deleteCommand);

        // Assert
        Assert.True(result.IsSuccess);

        GetAddressDto? addresses = await SendAsync(query);

        Assert.IsNull(addresses);
    }
}