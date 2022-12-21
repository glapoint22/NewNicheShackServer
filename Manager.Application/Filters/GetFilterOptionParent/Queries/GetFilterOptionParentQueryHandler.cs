using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Filters.GetFilterOptionParent.Queries
{
    public sealed class GetFilterOptionParentQueryHandler : IRequestHandler<GetFilterOptionParentQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetFilterOptionParentQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetFilterOptionParentQuery request, CancellationToken cancellationToken)
        {
            var filter = await _dbContext.FilterOptions
                .Where(x => x.Id == request.ChildId)
                .Select(x => new
                {
                    Id = x.FilterId,
                    x.Filter.Name
                }).SingleAsync();

            return Result.Succeeded(filter);
        }
    }
}