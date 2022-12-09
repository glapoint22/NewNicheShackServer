using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetSubproduct.Commands
{
    public sealed record SetSubproductCommandHandler : IRequestHandler<SetSubproductCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetSubproductCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetSubproductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.Subproducts
                    .Where(z => z.Id == request.SubproductId))
                .SingleAsync();

            product.SetSubproduct(request.Name, request.Description, request.ImageId, request.Value);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}