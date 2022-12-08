using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.UpdateSubproduct.Commands
{
    public sealed record UpdateSubproductCommandHandler : IRequestHandler<UpdateSubproductCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public UpdateSubproductCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdateSubproductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.Subproducts
                    .Where(z => z.Id == request.SubproductId))
                .SingleAsync();

            product.UpdateSubproduct(request.Name, request.Description, request.ImageId, request.Value);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}