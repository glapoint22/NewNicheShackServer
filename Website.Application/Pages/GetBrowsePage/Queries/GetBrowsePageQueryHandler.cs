using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.PageBuilder.Classes;
using Shared.PageBuilder.Enums;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Pages.GetBrowsePage.Queries
{
    public sealed class GetBrowsePageQueryHandler : IRequestHandler<GetBrowsePageQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IPageService _pageService;

        public GetBrowsePageQueryHandler(IWebsiteDbContext dbContext, IPageService pageService)
        {
            _dbContext = dbContext;
            _pageService = pageService;
        }



        public async Task<Result> Handle(GetBrowsePageQuery request, CancellationToken cancellationToken)
        {
            string pageContent = null!;

            // Set the params
            PageParams pageParams = new(
                request.NicheId,
                request.SubnicheId,
                request.SortBy,
                request.Filters,
                request.Page);

            // See if we have a custom browse page from a subniche
            string? pageId = await _dbContext.PageSubniches
                .Where(x => x.SubnicheId == request.SubnicheId)
                .Select(x => x.PageId)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);


            // See if we have a custom browse page from a niche
            if (pageId == null)
            {
                pageId = await _dbContext.PageNiches
                .Where(x => x.NicheId == request.NicheId)
                .Select(x => x.PageId)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);
            }

            if (pageId != null)
            {
                // Get the page content for the custom browse page
                pageContent = await _dbContext.Pages
                    .Where(x => x.Id == pageId)
                    .Select(x => x.Content)
                    .SingleAsync(cancellationToken: cancellationToken);
            }
            else
            {
                // Get the default grid page
                pageContent = await _dbContext.Pages
                        .Where(x => x.PageType == (int)PageType.Grid)
                        .Select(x => x.Content)
                        .SingleAsync(cancellationToken: cancellationToken);
            }

            WebPage page = await _pageService.GetPage(pageContent, pageParams);
            return Result.Succeeded(page);
        }
    }
}