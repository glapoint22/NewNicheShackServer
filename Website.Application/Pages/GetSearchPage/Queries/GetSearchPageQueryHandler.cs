using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.PageBuilder.Classes;
using Shared.PageBuilder.Enums;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Website.Application.Pages.GetSearchPage.Queries
{
    public sealed class GetSearchPageQueryHandler : IRequestHandler<GetSearchPageQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IPageService _pageService;

        public GetSearchPageQueryHandler(IWebsiteDbContext dbContext, IPageService pageService)
        {
            _dbContext = dbContext;
            _pageService = pageService;
        }

        public async Task<Result> Handle(GetSearchPageQuery request, CancellationToken cancellationToken)
        {
            string? pageContent = null!;

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


                var pageId = await _dbContext.PageKeywords
                    .Where(x => x.KeywordId == keywordId)
                    .Select(x => x.PageId)
                    .SingleOrDefaultAsync(cancellationToken: cancellationToken);

                if (pageId != null)
                {
                    pageContent = await _dbContext.Pages
                    .Where(x => x.Id == pageId)
                    .Select(x => x.Content)
                    .SingleOrDefaultAsync(cancellationToken: cancellationToken);
                }

            }



            // If page content is null, get the default grid page
            if (pageContent == null) pageContent = await _dbContext.Pages
                    .Where(x => x.PageType == (int)PageType.Grid)
                    .Select(x => x.Content)
                    .SingleAsync(cancellationToken: cancellationToken);


            WebPage page = await _pageService.GetPage(pageContent, pageParams);

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(page);

        }
    }
}