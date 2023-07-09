namespace Ecommerce.Application.Features.Orders.Commands.OrderCheckout;

public class OrderCheckoutCommandValidator : AbstractValidator<OrderCheckoutCommand>
{
    public OrderCheckoutCommandValidator()
    {
        RuleFor(oc => oc.UserId).NotEmpty().GreaterThan(0);
        RuleFor(oc => oc.CartItems).NotEmpty();
        RuleFor(oc => oc.ShippingPostalCode).NotEmpty();
        RuleFor(oc => oc.ShippingStreetName).NotEmpty();
        RuleFor(oc => oc.ShippingBuildingNumber).NotEmpty();
    }
}