using Microsoft.EntityFrameworkCore;
using Shared.PageBuilder.Classes;
using Shared.PageBuilder.Enums;
using Shared.QueryBuilder.Classes;
using Shared.QueryBuilder.Enums;
using System.Linq.Expressions;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Infrastructure.Services
{
    public sealed class PageService : IPageService
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly PageBuilder _pageBuilder;

        public PageService(IWebsiteDbContext dbContext, PageBuilder pageBuilder)
        {
            _dbContext = dbContext;
            _pageBuilder = pageBuilder;
        }

        public async Task<WebPage> GetPage(string searchTerm, int? nicheId, int? subnicheId, string? filters)
        {
            // Get the page content
            string pageContent = await _dbContext.Pages
                .OrderBy(x => x.PageType)
                .Where(x => x.Id == x.PageReferenceItems
                    .Where(z => z.KeywordGroupId == z.KeywordGroup.KeywordsInKeywordGroup
                        .Where(q => q.Keyword.Name == searchTerm)
                        .Select(q => q.KeywordGroupId)
                       .Single())
                    .Select(z => z.PageId)
                    .Single() || x.PageType == (int)PageType.Grid)
                .Select(x => x.Content)
                .FirstAsync();

            // Build the page from the page content
            WebPage page = _pageBuilder.BuildPage(pageContent);


            // Build the query
            var query = BuildQuery<Product>(searchTerm, nicheId, subnicheId, filters);


            // Set the data
            await _pageBuilder.SetData(page, query);

            return page;
        }

        public Task<WebPage> GetPage(int? nicheId, int? subnicheId, string? filters)
        {
            throw new NotImplementedException();
        }

        public Task<WebPage> GetPage(string pageId)
        {
            throw new NotImplementedException();
        }

        public Task<WebPage> GetPage(PageType pageType)
        {
            throw new NotImplementedException();
        }



        private static Expression<Func<T, bool>> BuildQuery<T>(string? searchTerm, int? nicheId, int? subnicheId, string? filters)
        {
            QueryBuilder queryBuilder = new();
            Query query = new();


            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Build a query row for the search term
                QueryElement searchQueryRow = queryBuilder.BuildQueryRow(QueryType.Search, searchTerm);
                query.Elements.Add(searchQueryRow);
            }





            // Niche
            if (nicheId != null)
            {
                if (query.Elements.Count > 0)
                {
                    // Add the logicalOperator
                    QueryElement row = queryBuilder.BuildQueryRow(LogicalOperatorType.And);
                    query.Elements.Add(row);
                }

                // Build a query row for the niche id
                QueryElement nicheQueryRow = queryBuilder.BuildQueryRow(QueryType.Niche, (int)nicheId);
                query.Elements.Add(nicheQueryRow);
            }



            // Subniche
            if (subnicheId != null)
            {
                if (query.Elements.Count > 0)
                {
                    // Add the logicalOperator
                    QueryElement row = queryBuilder.BuildQueryRow(LogicalOperatorType.And);
                    query.Elements.Add(row);
                }

                // Build a query row for the subniche id
                QueryElement subnicheIdQueryRow = queryBuilder.BuildQueryRow(QueryType.Niche, (int)subnicheId);
                query.Elements.Add(subnicheIdQueryRow);
            }


            // Filters
            if (!string.IsNullOrEmpty(filters))
            {
                if (query.Elements.Count > 0)
                {
                    // Add the logicalOperator
                    QueryElement row = queryBuilder.BuildQueryRow(LogicalOperatorType.And);
                    query.Elements.Add(row);
                }

                // Build a query row for the filters
                QueryElement filtersQueryRow = queryBuilder.BuildQueryRow(QueryType.Filters, filters);
                query.Elements.Add(filtersQueryRow);
            }

            return queryBuilder.BuildQuery<T>(query);
        }
    }
}