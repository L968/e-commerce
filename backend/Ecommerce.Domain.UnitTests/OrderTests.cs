using Ecommerce.Domain.Entities.OrderEntities;

namespace Ecommerce.Domain.UnitTests;

public class OrderTests
{
    [Fact]
    public void GivenValidData_ShouldCreateOrder()
    {
        // Arrange
        var cartItems = SampleData.GetCartItems();

        // Act
        var result = Order.Create(
            userId: 3,
            cartItems: cartItems,
            shippingPostalCode: "12345-678",
            shippingStreetName: "Main Street",
            shippingBuildingNumber: "123",
            shippingComplement: "Apt 456",
            shippingNeighborhood: "Downtown",
            shippingCity: "Cityville",
            shippingState: "ST",
            shippingCountry: "Countryland"
        );

        // Assert
        Assert.True(result.IsSuccess);

        Order order = result.Value;
        Assert.NotNull(order);
        Assert.Equal(3650, order.TotalAmount);
        Assert.Equal(0, order.Discount);
        Assert.Equal(30, order.ShippingCost);
    }
}
