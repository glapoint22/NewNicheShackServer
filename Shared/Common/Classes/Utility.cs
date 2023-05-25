using System.Text.RegularExpressions;

namespace Shared.Common.Classes
{
    public sealed class Utility
    {
        public static string GenerateUrlName(string name)
        {
            name = name.Replace("'", "");
            name = Regex.Replace(name, @"^[\W]|[\W]$", "");
            return Regex.Replace(name, @"[\W_]+", "-").ToLower();
        }


        public static string TextToHTML(string text)
        {
            Regex regex = new(@"^.+", RegexOptions.Multiline);
            MatchCollection matches = regex.Matches(text);

            string html = string.Empty;

            foreach (Match match in matches.ToList())
            {
                html += "<p>" + match.Value + "</p>";
            }

            return html;
        }
    }
}