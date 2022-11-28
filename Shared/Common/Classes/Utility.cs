using System.Text.RegularExpressions;

namespace Shared.Common.Classes
{
    public sealed class Utility
    {
        public static string GenerateUrlName(string name)
        {
            name = name.Replace("'", "");
            name = Regex.Replace(name, @"^[\W]|[\W]$", "");
            return Regex.Replace(name, @"[\W_]+", "-");
        }


        public static string GenerateId()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        }
    }
}