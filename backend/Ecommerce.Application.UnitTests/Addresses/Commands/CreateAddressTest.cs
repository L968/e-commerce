using Ecommerce.Application.Features.Addresses.Commands.CreateAddress;

namespace Ecommerce.Application.UnitTests.Addresses.Commands;

public class CreateAddressTest
{
    private readonly CreateAddressCommandValidator _validator;

    public CreateAddressTest()
    {
        _validator = new CreateAddressCommandValidator();
    }

    [Fact]
    public void ShouldHaveError_GivenInvalidData()
    {
        // Arrange
        var command = new CreateAddressCommand()
        {
            RecipientFullName = "",
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
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(address => address.RecipientFullName);
    }
}
