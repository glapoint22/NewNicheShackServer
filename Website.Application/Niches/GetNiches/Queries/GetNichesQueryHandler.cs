using MediatR;
using Microsoft.EntityFrameworkCore;

using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Shared.Common.Entities;

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