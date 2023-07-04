using Ecommerce.Application.Features.Addresses.Commands.CreateAddress;
using Ecommerce.Application.Features.Addresses.Commands.DeleteAddress;
using Ecommerce.Application.Features.Addresses.Queries;

namespace Ecommerce.Application.IntegrationTests.Addresses.Commands;

using static Testing;

public class DeleteAddressTests : BaseTestFixture
{
    [Test]
    public async Task ShouldDeleteAddress_GivenExistingAddress()
    {
        // Arrange
        RunAsRegularUser();

        Result<GetAddressDto> createResult = await SendAsync(new CreateAddressCommand()
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
        });

        GetAddressDto createdAddress = createResult.Value;

        // Act
        Result result = await SendAsync(new DeleteAddressCommand(createdAddress.Id!.Value));

        // Assert
        Assert.True(result.IsSuccess);

        GetAddressDto? addresses = await SendAsync(new GetAddressByIdAndUserIdQuery(createdAddress.Id!.Value));

        Assert.IsNull(addresses);
    }
}