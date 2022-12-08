using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.AddSubproduct.Commands
{
    public sealed class AddSubproductCommandHandler : IRequestHandler<AddSubproductCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public AddSubproductCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddSubproductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.Subproducts)
                .SingleAsync();

            product.AddSubproduct(request.Type);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded(product.Subproducts[product.Subproducts.Count - 1].Id);
        }
    }
}