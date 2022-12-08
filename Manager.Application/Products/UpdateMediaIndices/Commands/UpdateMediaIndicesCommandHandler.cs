using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.UpdateMediaIndices.Commands
{
    public sealed class UpdateMediaIndicesCommandHandler : IRequestHandler<UpdateMediaIndicesCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public UpdateMediaIndicesCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdateMediaIndicesCommand request, CancellationToken cancellationToken)
        {
            List<ProductMedia> productMediaList = await _dbContext.ProductMedia
                .Where(x => x.ProductId == request.ProductId)
                .ToListAsync();

            foreach (ProductMedia productMedia in productMediaList)
            {
                productMedia.Index = request.ProductMedia
                    .Where(x => x.Id == productMedia.Id)
                    .Select(x => x.Index)
                    .Single();

                if (productMedia.Index == 0)
                {
                    Product product = (await _dbContext.Products.FindAsync(request.ProductId))!;
                    product.SetImage(productMedia.MediaId);
                }
            }

            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}