﻿using Ecommerce.Application.Addresses.Commands.CreateAddress;
using Ecommerce.Application.Addresses.Commands.UpdateAddress;
using Ecommerce.Application.Addresses.Queries;
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

        GetAddressDto createdAddress = await SendAsync(new CreateAddressCommand()
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
        FluentResults.Result result = await SendAsync(new UpdateAddressCommand()
        {
            Id = createdAddress.Id!.Value,
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
        Assert.IsTrue(result.IsSuccess);

        Address? address = await FindAsync<Address>(createdAddress.Id!.Value);

        Assert.IsNotNull(address);
        Assert.AreEqual(address!.RecipientFullName, "Spencer Smith");
    }
}