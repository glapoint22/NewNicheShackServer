using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Filters.CheckDuplicateFilterOption.Queries
{
    public sealed class CheckDuplicateFilterOptionQueryHandler : IRequestHandler<CheckDuplicateFilterOptionQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public CheckDuplicateFilterOptionQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(CheckDuplicateFilterOptionQuery request, CancellationToken cancellationToken)
        {
            var filterId = await _dbContext.FilterOptions
                .Where(x => x.Id == request.ChildId)
                .Select(x => x.FilterId)
                .SingleAsync();

            if (await _dbContext.FilterOptions
                .AnyAsync(x => x.FilterId == filterId && x.Name.ToLower() == request.ChildName.Trim().ToLower()))
            {
                return Result.Succeeded(new
                {
                    id = request.ChildId,
                    name = request.ChildName,
                    parentId = filterId
                });
            }

            return Result.Succeeded();
        }
    }
}