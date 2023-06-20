namespace Ecommerce.Domain.Errors;

public static class DomainErrors
{
    public static class Cart
    {
        public static readonly Error CartAlreadyExists = new("A cart already exists for the current user");
        public static readonly Error CartItemNotBelongsToCart = new("CartItem does not belong to this cart");
        public static readonly Error CartNotFound = new("Cart not found");
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