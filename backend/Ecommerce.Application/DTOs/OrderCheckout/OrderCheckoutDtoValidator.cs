namespace Ecommerce.Application.DTOs.OrderCheckout;

public class OrderCheckoutDtoValidator : AbstractValidator<OrderCheckoutDto>
{
    public OrderCheckoutDtoValidator()
    {
        RuleFor(oc => oc.UserId).NotEmpty().GreaterThan(0);
        RuleFor(oc => oc.CartItems).NotEmpty();
        RuleFor(oc => oc.ShippingPostalCode).NotEmpty();
        RuleFor(oc => oc.ShippingStreetName).NotEmpty();
        RuleFor(oc => oc.ShippingBuildingNumber).NotEmpty();
    }
}
