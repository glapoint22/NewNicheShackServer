using Microsoft.EntityFrameworkCore;
using Shared.PageBuilder.Classes;
using Shared.PageBuilder.Enums;
using Shared.QueryBuilder.Classes;
using Shared.QueryBuilder.Enums;
using System.Linq.Expressions;
using Website.Application.Common.Interfaces;
using Shared.Common.Entities;

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

        public async Task<WebPage> GetPage(string searchTerm, int? nicheId, int? subnicheId, string? filters, int page, string? sortBy)
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
            WebPage webPage = _pageBuilder.BuildPage(pageContent);

            // Get the params
            PageParams pageParams = new(searchTerm, nicheId, subnicheId, page, sortBy, 40, filters);

            // Set the data
            await _pageBuilder.SetData(webPage, pageParams);

            return webPage;
        }



        public Task<WebPage> GetPage(int? nicheId, int? subnicheId, string? filters, int page, string? sortBy)
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
    }
}