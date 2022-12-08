using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.UpdatePrice.Commands
{
    public sealed class UpdatePriceCommandHandler : IRequestHandler<UpdatePriceCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public UpdatePriceCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdatePriceCommand request, CancellationToken cancellationToken)
        {
            Product product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.ProductPrices)
                .SingleAsync();

            product.UpdatePrice(request.Price);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}