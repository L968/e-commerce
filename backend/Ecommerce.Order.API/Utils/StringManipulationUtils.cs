using System.Text;

namespace Ecommerce.Order.API.Utils;

public static class StringManipulationUtils
{
    public static string AddSpacesBeforeUpperCase(string input)
    {
        StringBuilder result = new();

        foreach (char c in input)
        {
            if (char.IsUpper(c))
            {
                result.Append(' ');
            }

            result.Append(c);
        }

        return result.ToString().Trim();
    }
}
