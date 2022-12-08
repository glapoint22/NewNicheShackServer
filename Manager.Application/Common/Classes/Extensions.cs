using Manager.Domain.Entities;
using Shared.QueryBuilder;

namespace Manager.Application.Common.Classes
{
    public static class Extensions
    {
        // --------------------------------------------------------------------------- Where ---------------------------------------------------------------------------------
        public static IQueryable<T> Where<T>(this IQueryable<T> source, string searchTerm)
        {
            QueryBuilder queryBuilder = new();

            return source.Where(queryBuilder.BuildQuery<T>(searchTerm));
        }


        // ------------------------------------------------------------------------- Min Price --------------------------------------------------------------------------------
        public static double? MinPrice(this IEnumerable<ProductPrice> prices)
        {
            return prices.Count() > 0 ? prices.Min(x => x.Price) : 0;
        }







        // ------------------------------------------------------------------------- Max Price --------------------------------------------------------------------------------
        public static double MaxPrice(this IEnumerable<ProductPrice> prices)
        {
            return prices.Count() > 1 ? prices.Max(z => z.Price) : 0;
        }
    }
}