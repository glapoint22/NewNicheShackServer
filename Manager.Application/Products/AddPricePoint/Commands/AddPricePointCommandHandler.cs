using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.AddPricePoint.Commands
{
    public sealed class AddPricePointCommandHandler : IRequestHandler<AddPricePointCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public AddPricePointCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddPricePointCommand request, CancellationToken cancellationToken)
        {
            Product product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.PricePoints)
                .Include(x => x.ProductPrices)
                .SingleAsync();

            product.AddPricePoint();

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded(product.PricePoints[product.PricePoints.Count - 1].Id);
        }
    }
}