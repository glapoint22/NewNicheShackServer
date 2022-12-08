﻿using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Niches.GetNiches.Queries
{
    public sealed class GetNichesQueryHandler : IRequestHandler<GetNichesQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetNichesQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetNichesQuery request, CancellationToken cancellationToken)
        {
            var niches = await _dbContext.Niches
                .Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            return Result.Succeeded(niches);
        }
    }
}