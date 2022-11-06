using Shared.Common.Entities;
using System.Text.RegularExpressions;

namespace Website.Application.ProductOrders.GetOrders.Classes
{
    public static class Extensions
    {
        // --------------------------------------------------------------------------- Where ---------------------------------------------------------------------------------
        public static IQueryable<ProductOrder> Where(this IQueryable<ProductOrder> source, string filter, string? searchTerm = null)
        {
            // If there is a search term
            if (string.IsNullOrEmpty(searchTerm))
            {
                source = source.Where(x => x.Id == searchTerm);
            }
            else
            {
                // Get orders in a given time frame
                switch (filter)
                {
                    case "last-30":
                        source = source.Where(x => x.Date <= DateTime.UtcNow && x.Date > DateTime.UtcNow.AddDays(-30));
                        break;
                    case "6-months":
                        source = source.Where(x => x.Date <= DateTime.UtcNow && x.Date > DateTime.UtcNow.AddMonths(-6));
                        break;
                    default:
                        // Match the year (ex. year-2022)
                        Match match = Regex.Match(filter, @"(\d+)");

                        // Get all orders in a certain year
                        int year = int.Parse(match.Groups[1].Value);
                        source = source.Where(x => x.Date.Year == year);
                        break;
                }
            }

            return source;
        }
    }
}