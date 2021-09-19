using System.Linq;

namespace CombinedCodingStats
{
    public static class StringHelper
    {
        public static string[] negativeResponses = new string[] { "false", "no", "n", "nao", "não", "disable", "disabled" };
        public static string[] positiveResponses = new string[] { "true", "yes", "y", "sim", "s", "enable", "enabled" };

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);

        }
        public static bool HasNoValue(this string value)
        {
            return !value.HasValue();
        }

        public static bool IsNegative(this string response)
        {
            response = response?.ToLower();
            return negativeResponses.Contains(response);
        }

        public static bool IsPositive(this string response)
        {
            response = response?.ToLower();
            return positiveResponses.Contains(response);
        }
    }
}
