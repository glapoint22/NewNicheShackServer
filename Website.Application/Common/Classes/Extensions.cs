
using Shared.QueryBuilder.Classes;
using System.Text.RegularExpressions;
using Website.Domain.Entities;

namespace Website.Application.Common.Classes
{
    public static class Extensions
    {
        // -------------------------------------------------------------------------- Sort By ---------------------------------------------------------------------------------
        public static IOrderedQueryable<CollaboratorProduct> SortBy(this IQueryable<CollaboratorProduct> source, string? sortBy)
        {
            IOrderedQueryable<CollaboratorProduct> result;


            switch (sortBy)
            {
                case "price-asc":
                    result = source.OrderBy(x => x.Product.ProductPrices.Min(z => z.Price));
                    break;
                case "price-desc":
                    result = source.OrderByDescending(x => x.Product.ProductPrices.Max(z => z.Price));
                    break;
                case "rating":
                    result = source.OrderByDescending(x => x.Product.Rating);
                    break;
                case "title":
                    result = source.OrderBy(x => x.Product.Name);
                    break;
                default:
                    result = source.OrderByDescending(x => x.DateAdded);
                    break;
            }

            return result;
        }






        // -------------------------------------------------------------------------- Sort By ---------------------------------------------------------------------------------
        public static IOrderedQueryable<ProductReview> SortBy(this IQueryable<ProductReview> source, string? sortBy)
        {
            IOrderedQueryable<ProductReview> result;

            switch (sortBy)
            {
                case "low-to-high":
                    result = source
                        .OrderBy(x => x.Rating)
                        .ThenByDescending(x => x.Date);
                    break;

                case "newest-to-oldest":
                    result = source.OrderByDescending(x => x.Date);
                    break;

                case "oldest-to-newest":
                    result = source.OrderBy(x => x.Date);
                    break;

                case "most-helpful":
                    result = source
                        .OrderByDescending(x => x.Likes)
                        .ThenByDescending(x => x.Date);
                    break;

                default:
                    // High to low rating
                    result = source
                        .OrderByDescending(x => x.Rating)
                        .ThenByDescending(x => x.Date);
                    break;
            }

            return result;
        }






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







        // --------------------------------------------------------------------------- Where ---------------------------------------------------------------------------------
        public static IQueryable<T> Where<T>(this IQueryable<T> source, string searchTerm)
        {
            QueryBuilder queryBuilder = new QueryBuilder();

            return source.Where(queryBuilder.BuildQuery<T>(searchTerm));
        }
    }
}