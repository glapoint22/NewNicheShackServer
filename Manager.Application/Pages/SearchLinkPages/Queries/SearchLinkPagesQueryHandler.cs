using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.PageBuilder.Enums;

namespace Manager.Application.Pages.SearchLinkPages.Queries
{
    public sealed class SearchLinkPagesQueryHandler : IRequestHandler<SearchLinkPagesQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SearchLinkPagesQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SearchLinkPagesQuery request, CancellationToken cancellationToken)
        {
            var pages = await _dbContext.Pages
                .Where(x => x.PageType == (int)PageType.Custom || x.PageType == (int)PageType.Browse)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    Link = (x.PageType == (int)PageType.Custom ? "cp/" : "browse/") + x.UrlName + "/" + x.Id
                }).ToListAsync();
            return Result.Succeeded(pages);
        }
    }
}