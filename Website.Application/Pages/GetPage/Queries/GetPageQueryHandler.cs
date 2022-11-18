using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.PageBuilder.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Pages.GetPage.Queries
{
    public sealed class GetPageQueryHandler : IRequestHandler<GetPageQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;
        private readonly IPageService _pageService;

        public GetPageQueryHandler(IWebsiteDbContext dbContext, IPageService pageService)
        {
            _dbContext = dbContext;
            _pageService = pageService;
        }

        public async Task<Result> Handle(GetPageQuery request, CancellationToken cancellationToken)
        {
            string? pageContent = await _dbContext.Pages
                .Where(x => x.Id == request.Id || x.PageType == request.PageType)
                .Select(x => x.Content)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            if (pageContent == null) return Result.Failed("404");

            WebPage page = await _pageService.GetPage(pageContent);
            return Result.Succeeded(page);
        }
    }
}