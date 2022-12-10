using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Filters.GetFilterOptions.Queries
{
    public sealed class GetFilterOptionsQueryHandler : IRequestHandler<GetFilterOptionsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetFilterOptionsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetFilterOptionsQuery request, CancellationToken cancellationToken)
        {
            List<FilterOption> filterOptions = await _dbContext.FilterOptions
                .Where(x => x.FilterId == request.ParentId)
                .Include(x => x.ProductFilters
                    .Where(z => z.ProductId == request.ProductId))
                .ToListAsync();

            var productFilters = filterOptions
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    Checked = x.ProductFilters.Any()
                }).ToList();

            return Result.Succeeded(productFilters);
        }
    }
}