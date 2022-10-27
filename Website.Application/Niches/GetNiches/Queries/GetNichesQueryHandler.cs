using MediatR;
using Microsoft.EntityFrameworkCore;

using Website.Application.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

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
            List<Niche> niches = await _dbContext.Niches
                .AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);

            return Result.Succeeded(niches);
        }
    }
}