using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Website.Application.Niches.GetNiches.Queries
{
    public sealed class GetNichesQueryHandler : IRequestHandler<GetNichesQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public GetNichesQueryHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetNichesQuery request, CancellationToken cancellationToken)
        {
            var niches = await _dbContext.Niches
                .Where(x => !x.Disabled)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.UrlName
                })
                .ToListAsync(cancellationToken: cancellationToken);

            return Result.Succeeded(niches);
        }
    }
}