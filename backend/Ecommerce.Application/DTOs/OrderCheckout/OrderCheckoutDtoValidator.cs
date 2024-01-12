namespace Ecommerce.Application.DTOs.OrderCheckout;

public class OrderCheckoutDtoValidator : AbstractValidator<OrderCheckoutDto>
{
    public OrderCheckoutDtoValidator()
    {
        RuleFor(oc => oc.UserId).NotEmpty().GreaterThan(0);
        RuleFor(oc => oc.OrderCheckoutItems).NotEmpty();
        RuleForEach(oc => oc.OrderCheckoutItems).SetValidator(new OrderCheckoutCartItemDtoValidator());
        RuleFor(oc => oc.ShippingAddressId).NotEmpty();
    }

    public class OrderCheckoutCartItemDtoValidator : AbstractValidator<OrderCheckoutItemDto>
    {
        public OrderCheckoutCartItemDtoValidator()
        {
            RuleFor(ci => ci.ProductCombinationId).NotEmpty();
            RuleFor(ci => ci.Quantity).NotEmpty().GreaterThan(0);
        }
    }
}
