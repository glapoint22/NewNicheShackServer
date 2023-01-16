using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.GetAllSubniches.Queries
{
    public sealed class GetAllSubnichesQueryHandler : IRequestHandler<GetAllSubnichesQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetAllSubnichesQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetAllSubnichesQuery request, CancellationToken cancellationToken)
        {
            var subniches = await _dbContext.Subniches
                .OrderBy(x => x.Name)
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            return Result.Succeeded(subniches);
        }
    }
}