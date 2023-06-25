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
        public static readonly Error InvalidLengthValue = new("Invalid length value");
        public static readonly Error InvalidWidthValue = new("Invalid width value");
        public static readonly Error InvalidHeightValue = new("Invalid height value");
        public static readonly Error InvalidWeightValue = new("Invalid weight value");
    }

    public static class ProductDiscount
    {
        public static readonly Error DiscountAlreadyExists = new("An active discount already exists for this product");
        public static readonly Error DiscountStartDateInPast = new("Discount start date cannot be in the past");
        public static readonly Error DiscountEndDateMustBeAfterStartDate = new("Discount end date must be later than start date");
        public static readonly Error InvalidDiscountValue = new("Invalid discount value");
        public static readonly Error CannotUpdateExpiredDiscount = new("Cannot update an expired discount");
        public static readonly Error MaximumDiscountAmountExceedsValue = new("Maximum discount amount cannot be greater than or equal to the discount value");
        public static readonly Error DiscountDurationTooShort = new("Discount duration must be at least 5 minutes");
        public static readonly Error DiscountPercentageExceedsLimit = new("Discount percentage exceeds the limit of 80%");
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