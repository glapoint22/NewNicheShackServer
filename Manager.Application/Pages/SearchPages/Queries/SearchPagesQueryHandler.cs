﻿using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Shared.QueryBuilder;

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
            QueryBuilder queryBuilder = new();

            var query = queryBuilder.BuildQuery<Page>(request.SearchTerm);
            var pages = await _dbContext.Pages
                .Where(query)
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            return Result.Succeeded(pages);
        }
    }
}