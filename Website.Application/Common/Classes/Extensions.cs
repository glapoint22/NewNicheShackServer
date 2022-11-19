using Microsoft.EntityFrameworkCore;
using Website.Domain.Entities;

namespace Website.Application.Common.Classes
{
    public static class Extensions
    {
        // -------------------------------------------------------------------------- Sort By ---------------------------------------------------------------------------------
        public static IOrderedQueryable<Product> SortBy(this IQueryable<Product> source, string? searchTerm, string? sortBy = null)
        {
            IOrderedQueryable<Product> result;


            switch (sortBy)
            {
                case "price-asc":
                    result = source.OrderBy(x => x.ProductPrices.Min(z => z.Price));
                    break;
                case "price-desc":
                    result = source.OrderByDescending(x => x.ProductPrices.Max(z => z.Price));
                    break;
                case "rating":
                    result = source.OrderByDescending(x => x.Rating);
                    break;
                case "newest":
                    result = source.OrderByDescending(x => x.Date);
                    break;
                default:
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        result = source.OrderBy(x => x.Name.ToLower().StartsWith(searchTerm.ToLower()) ? (x.Name.ToLower() == searchTerm.ToLower() ? 0 : 1) :
                            EF.Functions.Like(x.Name, "% " + searchTerm + " %") ? 2 : 3);
                    }
                    else
                    {
                        result = source.OrderByDescending(x => x.Date);
                    }


                    break;
            }

            return result;
        }






        // --------------------------------------------------------------------------- Where ---------------------------------------------------------------------------------
        public static IQueryable<T> Where<T>(this IQueryable<T> source, string searchTerm)
        {
            WebsiteQueryBuilder queryBuilder = new();

            return source.Where(queryBuilder.BuildQuery<T>(searchTerm));
        }






        // ------------------------------------------------------------------------- Min Price --------------------------------------------------------------------------------
        public static double MinPrice(this IEnumerable<ProductPrice> prices)
        {
            return prices.Min(x => x.Price);
        }







        // ------------------------------------------------------------------------- Max Price --------------------------------------------------------------------------------
        public static double MaxPrice(this IEnumerable<ProductPrice> prices)
        {
            return prices.Count() > 1 ? prices.Max(z => z.Price) : 0;
        }
    }
}