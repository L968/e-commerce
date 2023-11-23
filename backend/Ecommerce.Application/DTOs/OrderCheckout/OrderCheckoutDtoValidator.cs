namespace Ecommerce.Application.DTOs.OrderCheckout;

public class OrderCheckoutDtoValidator : AbstractValidator<OrderCheckoutDto>
{
    public OrderCheckoutDtoValidator()
    {
        RuleFor(oc => oc.UserId).NotEmpty().GreaterThan(0);
        RuleFor(oc => oc.CartItems).NotEmpty();
        RuleForEach(oc => oc.CartItems).SetValidator(new OrderCheckoutCartItemDtoValidator());
        RuleFor(oc => oc.ShippingPostalCode).NotEmpty();
        RuleFor(oc => oc.ShippingStreetName).NotEmpty();
        RuleFor(oc => oc.ShippingBuildingNumber).NotEmpty();
    }

    public class OrderCheckoutCartItemDtoValidator : AbstractValidator<OrderCheckoutCartItemDto>
    {
        public OrderCheckoutCartItemDtoValidator()
        {
            RuleFor(ci => ci.CartId).NotEmpty().GreaterThan(0);
            RuleFor(ci => ci.ProductCombinationId).NotEmpty();
            RuleFor(ci => ci.Quantity).NotEmpty().GreaterThan(0);
        }
    }
}
