using Shared.QueryService.Enums;
using System.Linq.Expressions;
using Website.Application.Common.Interfaces;

namespace Shared.QueryService.Classes
{
    public sealed class WebsiteQueryService : QueryServiceBase, IQueryService
    {
        public Expression<Func<T, bool>> BuildQuery<T>(string? searchTerm = null, string? filters = null, int? nicheId = null, int? subnicheId = null)
        {
            Query query = new();

            // Search Term
            if (searchTerm != null)
            {
                QueryRow row = new()
                {
                    QueryType = QueryType.Search,
                    StringValue = searchTerm,
                };

                QueryElement queryElement = new()
                {
                    QueryElementType = QueryElementType.QueryRow,
                    QueryRow = row
                };

                query.Elements.Add(queryElement);
            }


            // Filters
            if (filters != null)
            {

            }




            // Niche Id
            if (nicheId != null)
            {
                QueryRow row = new()
                {
                    QueryType = QueryType.Niche,
                    Integer = nicheId,
                };

                QueryElement queryElement = new()
                {
                    QueryElementType = QueryElementType.QueryRow,
                    QueryRow = row
                };

                query.Elements.Add(queryElement);
            }



            // Subniche Id
            if (subnicheId != null)
            {
                QueryRow row = new()
                {
                    QueryType = QueryType.Subniche,
                    Integer = subnicheId,
                };

                QueryElement queryElement = new()
                {
                    QueryElementType = QueryElementType.QueryRow,
                    QueryRow = row
                };

                query.Elements.Add(queryElement);
            }


            // Build the query using the query object
            return BuildQuery<T>(query);
        }
    }
}