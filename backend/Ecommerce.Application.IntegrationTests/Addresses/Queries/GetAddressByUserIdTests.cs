using Ecommerce.Application.Addresses.Commands.CreateAddress;
using Ecommerce.Application.Addresses.Queries;

namespace Ecommerce.Application.IntegrationTests.Addresses.Queries;

using static Testing;

public class GetAddressByUserIdTests : BaseTestFixture
{
    [Test]
    public async Task ShoudlReturnAllUserAddresses_WhenTableHasData()
    {
        // Arrange
        RunAsRegularUser();

        await SendAsync(new CreateAddressCommand()
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

        // Act
        IEnumerable<GetAddressDto> addresses = await SendAsync(new GetAddressByUserIdQuery());

        // Assert
        Assert.NotNull(addresses);
        Assert.AreEqual(addresses.Count(), 1);
    }
}