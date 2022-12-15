using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.ProductGroups.GetProductGroups.Queries
{
    public sealed class GetProductGroupsQueryHandler : IRequestHandler<GetProductGroupsQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetProductGroupsQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetProductGroupsQuery request, CancellationToken cancellationToken)
        {
            var productGroups = await _dbContext.ProductGroups
                .OrderBy(x => x.Name)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    Checked = x.ProductsInProductGroup.Any(z => z.ProductId == request.ProductId),
                }).ToListAsync();

            return Result.Succeeded(productGroups);
        }
    }
}