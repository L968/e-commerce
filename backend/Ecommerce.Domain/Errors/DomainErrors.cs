namespace Ecommerce.Domain.Errors;

public static class DomainErrors
{
    public static class Address
    {
        public static readonly Error InvalidRecipientPhoneNumber = new("RecipientPhoneNumber must contain numbers only");
    }

    public static class Cart
    {
        public static readonly Error CartAlreadyExists = new("A cart already exists for the current user");
        public static readonly Error CartItemNotBelongsToCart = new("CartItem does not belong to this cart");
        public static readonly Error CartNotFound = new("Cart not found");
    }

    public static class CartItem
    {
        public static readonly Error InvalidQuantity = new("Invalid quantity value");
    }

    public static class Product
    {
        public static readonly Error InvalidPriceValue = new("Invalid price value");
        public static readonly Error EmptyImagePathList = new("Empty image path list");
        public static readonly Error InvalidLengthValue = new("Invalid length value");
        public static readonly Error InvalidWidthValue = new("Invalid width value");
        public static readonly Error InvalidHeightValue = new("Invalid height value");
        public static readonly Error InvalidWeightValue = new("Invalid weight value");
        public static Error InactiveProduct(Guid id) => new($"Product {id} is inactive");
    }

    public static class ProductCombination
    {
        public static readonly Error CombinationAlreadyExists = new("Combination already exists for the product");
    }

    public static class ProductCategory
    {
        public static readonly Error EmptyVariantList = new("Empty variant list");
    }

    public static class ProductDiscount
    {
        public static readonly Error DiscountStartDateInPast = new("Discount start date cannot be in the past");
        public static readonly Error DiscountEndDateMustBeAfterStartDate = new("Discount end date must be later than start date");
        public static readonly Error InvalidDiscountValue = new("Invalid discount value");
        public static readonly Error CannotUpdateExpiredDiscount = new("Cannot update an expired discount");
        public static readonly Error MaximumDiscountAmountExceedsValue = new("Maximum discount amount cannot be greater than or equal to the discount value");
        public static readonly Error DiscountDurationTooShort = new("Discount duration must be at least 5 minutes");
        public static readonly Error DiscountPercentageExceedsLimit = new("Discount percentage exceeds the limit of 80%");
        public static readonly Error DiscountHasOverlap = new("This discount conflicts with other existing discounts");
        public static readonly Error MaximumFixedDiscountExceeded = new ("Maximum discount amount for fixed discount exceeded (80% of original price)");
    }

    public static class ProductInventory
    {
        public static readonly Error InvalidQuantity = new("Invalid quantity value");
        public static readonly Error InsufficientStock = new("Insufficient stock");
    }

    public static class Variant
    {
        public static readonly Error EmptyOptionList = new("Empty option list");
    }

    public static class Order
    {
        public static readonly Error InvalidUserId = new("Invalid User Id");
        public static readonly Error EmptyProductList = new("Product list cannot be empty");
        public static readonly Error InsufficientStock = new("Insufficient stock for some products");
        public static readonly Error InactiveProduct = new("Inactive product cannot be added to the order");
        public static readonly Error DiscountUnitNotImplemented = new("Discount unit is not implemented");
        public static readonly Error CannotAddItemToCancelledOrder = new("Cannot add item to a cancelled order");
        public static readonly Error InvalidPaymentStatus = new("Invalid payment status");
        public static readonly Error OrderNotFoundByExternalPaymentId = new("Failed to find an order with the provided payment token");
    }

    public static class PayPal
    {
        public static readonly Error OrderNotFound = new("Failed to retrieve PayPal order details. Please try again");
        public static readonly Error OrderNotApproved = new("The PayPal order status is not approved. Cannot process payment");
        public static readonly Error CheckoutUrlNotFound = new("PayPal checkout url not found");
    }

    public static Error NotFound(string entityName, int id)
    {
        return new Error($"{entityName} with ID {id} not found");
    }

    public static Error NotFound(string entityName, Guid guid)
    {
        return new Error($"{entityName} with Guid {guid} not found");
    }
}

//public class DomainError : Error
//{
//    public DomainError(
//        string message,
//        [CallerLineNumber] int lineNumber = 0,
//        [CallerMemberName] string memberName = "")
//    : base(message)
//    {
//        Metadata.Add("Line", lineNumber);
//        Metadata.Add("MemberName", memberName);
//    }
//}
