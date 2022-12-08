using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.UpdatePricePoint.Commands
{
    public sealed class UpdatePricePointCommandHandler : IRequestHandler<UpdatePricePointCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public UpdatePricePointCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdatePricePointCommand request, CancellationToken cancellationToken)
        {
            Product product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.PricePoints
                    .Where(z => z.Id == request.PricePoint.Id))
                    .ThenInclude(z => z.ProductPrice)
                .SingleAsync();

            product.UpdatePricePoint(request.PricePoint);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}