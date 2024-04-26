using System.Text;

namespace Ecommerce.Order.API.Utils;

public static class StringExtensions
{
    public static string ToSnakeCase(this string text)
    {
        if (text is null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (text.Length < 2)
        {
            return text;
        }

        var sb = new StringBuilder();
        sb.Append(char.ToLowerInvariant(text[0]));

        for (int i = 1; i < text.Length; ++i)
        {
            char c = text[i];
            if (char.IsUpper(c))
            {
                sb.Append('_');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }

    public static string AddSpacesBeforeUpperCase(this string input)
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
