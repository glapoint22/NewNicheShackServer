using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.RemoveSubproduct.Commands
{
    public sealed class RemoveSubproductCommandHandler : IRequestHandler<RemoveSubproductCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public RemoveSubproductCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RemoveSubproductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.Subproducts
                    .Where(z => z.Id == request.SubproductId))
                .SingleAsync();

            product.RemoveSubproduct();

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}