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
        private readonly PageBuilder _pageBuilder;

        public GetPageQueryHandler(IWebsiteDbContext dbContext, PageBuilder pageBuilder)
        {
            _dbContext = dbContext;
            _pageBuilder = pageBuilder;
        }

        public async Task<Result> Handle(GetPageQuery request, CancellationToken cancellationToken)
        {
            string? pageContent = await _dbContext.Pages
                .Where(x => x.Id == request.Id || x.PageType == request.PageType)
                .Select(x => x.Content)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            if (pageContent == null) return Result.Failed("404");

            WebPage page = await _pageBuilder.BuildPage(pageContent);

            return Result.Succeeded(page);
        }
    }
}