using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Niches.GetSubniches.Queries
{
    public sealed class GetSubnichesQueryHandler : IRequestHandler<GetSubnichesQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public GetSubnichesQueryHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetSubnichesQuery request, CancellationToken cancellationToken)
        {
            var subniches = await _dbContext.Subniches
                .OrderBy(x => x.Name)
                .Where(x => x.NicheId == request.NicheId && !x.Disabled)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.UrlName
                })
                .ToListAsync();

            return Result.Succeeded(subniches);
        }
    }
}