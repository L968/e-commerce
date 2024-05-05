using Bogus;
using Ecommerce.Domain.Entities.OrderEntities;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.UnitTests.Orders;

public class OrderTests
{
    private readonly Faker<Order> _orderFaker;

    public OrderTests()
    {
        _orderFaker = new Faker<Order>()
            .CustomInstantiator(f => Order.Create(
                userId: f.Random.Number(1, 1000),
                cartItems: [],
                paymentMethod: f.PickRandom<PaymentMethod>(),
                shippingCost: f.Random.Decimal(1, 100),
                shippingPostalCode: f.Address.ZipCode("#####-###"),
                shippingStreetName: f.Address.StreetName(),
                shippingBuildingNumber: f.Address.BuildingNumber(),
                shippingComplement: f.Address.SecondaryAddress(),
                shippingNeighborhood: f.Address.County(),
                shippingCity: f.Address.City(),
                shippingState: f.Address.StateAbbr(),
                shippingCountry: f.Address.Country()
            ).Value);
    }

    [Fact]
    public void GivenValidData_CreateOrderShouldReturnOrder()
    {
        // Arrange
        var order = _orderFaker.Generate();

        // Assert
        Assert.NotNull(order);
        Assert.InRange(order.UserId, 1, 1000);
        Assert.Equal(OrderStatus.PendingPayment, order.Status);
        Assert.Empty(order.Items);
        Assert.Null(order.ExternalPaymentId);
    }
}
