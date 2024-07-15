using Ecommerce.Application.DTOs.Addresses;
using Ecommerce.Application.Features.Addresses.Commands.CreateAddress;
using Ecommerce.Application.Features.Addresses.Commands.UpdateAddress;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.IntegrationTests.Addresses.Commands;

using static Testing;

public class UpdateAddressTests : BaseTestFixture
{
    [Test]
    public async Task ShouldUpdateAddress_GivenValidData()
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
        await SendAsync(new UpdateAddressCommand()
        {
            Id = createdAddress.Id,
            RecipientFullName = "Spencer Smith",
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

        // Assert
        Address? address = await FindAsync<Address>(createdAddress.Id);

        Assert.IsNotNull(address);
        Assert.AreEqual(address!.RecipientFullName, "Spencer Smith");
    }
}
