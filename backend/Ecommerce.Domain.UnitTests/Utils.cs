namespace Ecommerce.Domain.UnitTests;

internal static class Utils
{
    internal static void SetPrivateProperty(object instance, string propertyName, object value)
    {
        var property = instance.GetType().GetProperty(propertyName);
        property?.SetValue(instance, value);
    }
}
