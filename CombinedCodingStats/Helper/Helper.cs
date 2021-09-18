using System;
using System.Linq;

namespace CombinedCodingStats.Helper
{
    public static class Helper
    {
        public static string[] negativeResponses = new string[] { "false", "no", "nao", "não", "disable" };
        public static bool IsNegativeResponse(this string response)
        {
            response = response.ToLower();

            return negativeResponses.Contains(response);
        }
    }
}
