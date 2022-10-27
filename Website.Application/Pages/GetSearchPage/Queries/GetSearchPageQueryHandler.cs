using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.PageBuilder.Classes;
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
            }


            // Get the search page
            WebPage page = await _pageService.GetPage(request.SearchTerm, request.NicheId, request.SubnicheId, request.Filters);

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(page);
        }
    }
}