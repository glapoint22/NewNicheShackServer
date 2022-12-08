using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.DeletePricePoint.Commands
{
    public sealed class DeletePricePointCommandHandler : IRequestHandler<DeletePricePointCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeletePricePointCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeletePricePointCommand request, CancellationToken cancellationToken)
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