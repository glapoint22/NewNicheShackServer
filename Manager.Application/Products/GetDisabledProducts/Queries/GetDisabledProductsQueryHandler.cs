using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Products.GetDisabledProducts.Queries
{
    public sealed class GetDisabledProductsQueryHandler : IRequestHandler<GetDisabledProductsQuery, Result>
    {
        private readonly IWebsiteDbContext _dbContext;

        public GetDisabledProductsQueryHandler(IWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetDisabledProductsQuery request, CancellationToken cancellationToken)
        {
            var disabledProducts = await _dbContext.Products
                .Where(x => x.Disabled)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Hoplink,
                    Image = x.Media.Thumbnail
                }).ToListAsync();

            return Result.Succeeded(disabledProducts);
        }
    }
}