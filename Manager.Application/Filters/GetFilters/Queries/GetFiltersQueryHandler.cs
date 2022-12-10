using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Filters.GetFilters.Queries
{
    public sealed class GetFiltersQueryHandler : IRequestHandler<GetFiltersQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetFiltersQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetFiltersQuery request, CancellationToken cancellationToken)
        {
            var filters = await _dbContext.Filters
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            return Result.Succeeded(filters);
        }
    }
}