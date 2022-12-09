using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.RemovePricePoint.Commands
{
    public sealed class RemovePricePointCommandHandler : IRequestHandler<RemovePricePointCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public RemovePricePointCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RemovePricePointCommand request, CancellationToken cancellationToken)
        {
            Product product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.PricePoints
                    .Where(z => z.Id == request.PricePointId))
                    .ThenInclude(z => z.ProductPrice)
                .SingleAsync();

            product.RemovePricePoint();

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}