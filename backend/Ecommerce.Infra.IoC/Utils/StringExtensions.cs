using Newtonsoft.Json.Linq;

namespace Ecommerce.Infra.IoC.Utils;

public static class StringExtensions
{
    public static bool IsValidJson(this string text)
    {
        text = text.Trim();

        if (text.StartsWith('{') && text.EndsWith('}') || text.StartsWith('{') && text.EndsWith('}'))
        {
            try
            {
                JToken.Parse(text);
                return true;
            }
            catch
            {
                return false;
            }
        }

        return false;
    }
}
