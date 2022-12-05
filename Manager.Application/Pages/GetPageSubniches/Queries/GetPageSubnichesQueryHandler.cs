using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Pages.GetPageSubniches.Queries
{
    public sealed class GetPageSubnichesQueryHandler : IRequestHandler<GetPageSubnichesQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetPageSubnichesQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetPageSubnichesQuery request, CancellationToken cancellationToken)
        {
            var subnicheIds = await _dbContext.PageSubniches
                .Where(x => x.PageId == request.PageId)
                .Select(x => x.SubnicheId)
                .ToListAsync();

            var subniches = await _dbContext.Subniches
                .Where(x => subnicheIds.Contains(x.Id))
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            return Result.Succeeded(subniches);
        }
    }
}