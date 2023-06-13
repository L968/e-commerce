using Ecommerce.Application.Addresses.Queries;

namespace Ecommerce.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class GetAddressTests : BaseTestFixture
{
    [Test]
    public async Task ShoudlReturnAllUserAddresses_WhenTableHasData()
    {
        // Arrange
        RunAsRegularUser();
        var query = new GetAddressByUserIdQuery();

        // Act
        IEnumerable<GetAddressDto> addresses = await SendAsync(query);

        // Assert
        Assert.NotNull(addresses);
    }
}