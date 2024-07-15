namespace Ecommerce.Domain.Errors;

public static class DomainErrors
{
    public static class Address
    {
        public const string InvalidRecipientPhoneNumber = "RecipientPhoneNumber must contain numbers only";
    }

    public static class Cart
    {
        public const string CartAlreadyExists = "A cart already exists for the current user";
        public const string CartItemNotBelongsToCart = "CartItem does not belong to this cart";
        public static string CartNotFound(int userId) => $"Cart not found for user {userId}";
    }

    public static class CartItem
    {
        public const string InvalidQuantity = "Invalid quantity value";
    }

    public static class Product
    {
        public const string InvalidPriceValue = "Invalid price value";
        public const string EmptyImagePathList = "Empty image path list";
        public const string InvalidLengthValue = "Invalid length value";
        public const string InvalidWidthValue = "Invalid width value";
        public const string InvalidHeightValue = "Invalid height value";
        public const string InvalidWeightValue = "Invalid weight value";
        public static string InactiveProduct(Guid id) => $"Product {id} is inactive";
    }

    public static class ProductCombination
    {
        public const string CombinationAlreadyExists = "Combination already exists for this product";
    }

    public static class ProductCategory
    {
        public const string EmptyVariantList = "Empty variant list";
    }

    public static class ProductDiscount
    {
        public const string DiscountStartDateInPast = "Discount start date cannot be in the past";
        public const string DiscountEndDateMustBeAfterStartDate = "Discount end date must be later than start date";
        public const string InvalidDiscountValue = "Invalid discount value";
        public const string CannotUpdateExpiredDiscount = "Cannot update an expired discount";
        public const string MaximumDiscountAmountExceedsValue = "Maximum discount amount cannot be greater than or equal to the discount value";
        public const string DiscountDurationTooShort = "Discount duration must be at least 5 minutes";
        public const string DiscountPercentageExceedsLimit = "Discount percentage exceeds the limit of 80%";
        public const string DiscountHasOverlap = "This discount conflicts with other existing discounts";
        public const string MaximumFixedDiscountExceeded = "Maximum discount amount for fixed discount exceeded (80% of original price)";
    }

    public static class ProductInventory
    {
        public const string InvalidQuantity = "Invalid quantity value";
        public const string InsufficientStock = "Insufficient stock";
    }

    public static class ProductReview
    {
        public const string InvalidUserId = "Invalid UserId";
        public const string InvalidProductId = "Invalid ProductId";
        public const string InvalidRatingRange = "Rating must be between 1 and 5";
    }

    public static class Variant
    {
        public const string EmptyOptionList = "Empty option list";
    }

    public static class Order
    {
        public const string InvalidUserId = "Invalid User Id";
        public const string EmptyProductList = "Product list cannot be empty";
        public const string InsufficientStock = "Insufficient stock for some products";
        public const string InactiveProduct = "Inactive product cannot be added to the order";
        public const string DiscountUnitNotImplemented = "Discount unit is not implemented";
        public const string CannotAddItemToCancelledOrder = "Cannot add item to a cancelled order";
        public const string InvalidPaymentStatus = "Invalid payment status";
        public static string OrderNotFoundByExternalPaymentId(string token) => $"Failed to find an order with the provided payment token: {token}";
    }

    public static class PayPal
    {
        public const string OrderNotFound = "Failed to retrieve PayPal order details. Please try again";
        public const string OrderNotApproved = "The PayPal order status is not approved. Cannot process payment";
        public const string CheckoutUrlNotFound = "PayPal checkout url not found";
    }

    public static string NotFound(string entityName, int id)
    {
        return $"{entityName} with ID {id} not found";
    }

    public static string NotFound(string entityName, Guid guid)
    {
        return $"{entityName} with Guid {guid} not found";
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
