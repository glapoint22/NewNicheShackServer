using Shared.QueryBuilder.Classes;

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
    }
}