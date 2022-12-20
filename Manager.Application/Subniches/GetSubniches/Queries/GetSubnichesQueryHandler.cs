using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.GetSubniches.Queries
{
    public sealed class GetSubnichesQueryHandler : IRequestHandler<GetSubnichesQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetSubnichesQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetSubnichesQuery request, CancellationToken cancellationToken)
        {
            var subniches = await _dbContext.Subniches
                .OrderBy(x => x.Name)
                .Where(x => x.NicheId == request.ParentId)
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            return Result.Succeeded(subniches);
        }
    }
}