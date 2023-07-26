using Manager.Application.Common.Classes;
using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.SearchLinkSubniches.Queries
{
    public sealed class SearchLinkSubnichesQueryHandler : IRequestHandler<SearchLinkSubnichesQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SearchLinkSubnichesQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SearchLinkSubnichesQuery request, CancellationToken cancellationToken)
        {
            var subniches = await _dbContext.Subniches
                .Where(request.SearchTerm)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    Link = "browse?subnicheName=" + x.UrlName + "&subnicheId=" + x.Id
                }).ToListAsync();

            return Result.Succeeded(subniches);
        }
    }
}
