using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.QueryBuilder;

namespace Manager.Application.Vendors.SearchVendors.Queries
{
    public sealed class SearchVendorsQueryHandler : IRequestHandler<SearchVendorsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SearchVendorsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SearchVendorsQuery request, CancellationToken cancellationToken)
        {
            QueryBuilder queryBuilder = new();

            var query = queryBuilder.BuildQuery<Vendor>(request.SearchTerm);
            var vendors = await _dbContext.Vendors
                .Where(query)
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            return Result.Succeeded(vendors);
        }
    }
}