using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.GetProductParent.Queries
{
    public sealed class GetProductParentQueryHandler : IRequestHandler<GetProductParentQuery, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public GetProductParentQueryHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(GetProductParentQuery request, CancellationToken cancellationToken)
        {
            var subniche = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Select(x => new
                {
                    Id = x.SubnicheId,
                    x.Subniche.Name
                }).SingleAsync();

            return Result.Succeeded(subniche);
        }
    }
}