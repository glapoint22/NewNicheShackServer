using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetPrice.Commands
{
    public sealed class SetPriceCommandHandler : IRequestHandler<SetPriceCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetPriceCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetPriceCommand request, CancellationToken cancellationToken)
        {
            Product product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.ProductPrices)
                .SingleAsync();

            product.SetPrice(request.Price);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}