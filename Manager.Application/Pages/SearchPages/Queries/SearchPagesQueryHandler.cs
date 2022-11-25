using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.QueryBuilder.Classes;

namespace Manager.Application.Pages.SearchPages.Queries
{
    public sealed class SearchPagesQueryHandler : IRequestHandler<SearchPagesQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SearchPagesQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SearchPagesQuery request, CancellationToken cancellationToken)
        {
            QueryBuilder queryBuilder = new QueryBuilder();

            var pages = await _dbContext.Pages
                .Where(queryBuilder.BuildQuery<Page>(request.SearchTerm))
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            return Result.Succeeded(pages);
        }
    }
}