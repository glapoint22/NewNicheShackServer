using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.PageBuilder.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Shared.Common.Entities;
using Shared.PageBuilder.Enums;

namespace Website.Application.Pages.GetSearchPage.Queries
{
    public sealed class GetSearchPageQueryHandler : IRequestHandler<GetSearchPageQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly PageBuilder _pageBuilder;

        public GetSearchPageQueryHandler(IWebsiteDbContext dbContext, PageBuilder pageBuilder)
        {
            _dbContext = dbContext;
            _pageBuilder = pageBuilder;
        }

        public async Task<Result> Handle(GetSearchPageQuery request, CancellationToken cancellationToken)
        {
            string pageContent = null!;

            // Set the params
            PageParams pageParams = new(
                request.SearchTerm,
                request.NicheId,
                request.SubnicheId,
                request.SortBy,
                request.Filters,
                request.Page);

            int keywordId = await _dbContext.Keywords
                .Where(x => x.Name == request.SearchTerm)
                .Select(x => x.Id)
                .SingleOrDefaultAsync();


            if (keywordId > 0)
            {
                // Add the keyword to the search volumes
                _dbContext.KeywordSearchVolumes.Add(new KeywordSearchVolume
                {
                    KeywordId = keywordId,
                    Date = DateTime.UtcNow
                });

                // Get the custom search page
                pageContent = await _dbContext.Pages
                    .Where(x => x.Id == x.PageReferenceItems
                        .Where(z => z.KeywordGroupId == z.KeywordGroup.KeywordsInKeywordGroup
                            .Where(q => q.KeywordId == keywordId)
                            .Select(q => q.KeywordGroupId)
                           .Single())
                        .Select(z => z.PageId)
                        .Single())
                    .Select(x => x.Content)
                    .SingleAsync(cancellationToken: cancellationToken);
            }



            // If page content is null, get the default grid page
            if (pageContent == null) pageContent = await _dbContext.Pages
                    .Where(x => x.PageType == (int)PageType.Grid)
                    .Select(x => x.Content)
                    .SingleAsync(cancellationToken: cancellationToken);


            WebPage page = await _pageBuilder.BuildPage(pageContent, pageParams);

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(page);
        }
    }
}