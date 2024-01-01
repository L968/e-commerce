using Ecommerce.Application.DTOs.Addresses;
using Ecommerce.Application.Features.Addresses.Commands.CreateAddress;
using Ecommerce.Application.Features.Addresses.Queries;

namespace Ecommerce.Application.IntegrationTests.Addresses.Queries;

using static Testing;

public class GetAddressByIdAndUserIdTests : BaseTestFixture
{
    [Test]
    public async Task ShoudlReturnAllUserAddresses_WhenTableHasData()
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
        GetAddressDto? addresses = await SendAsync(new GetAddressByIdAndUserIdQuery(createdAddress.Id!.Value));

        // Assert
        Assert.NotNull(addresses);
        Assert.AreEqual(addresses!.Id, createdAddress.Id);
    }
}