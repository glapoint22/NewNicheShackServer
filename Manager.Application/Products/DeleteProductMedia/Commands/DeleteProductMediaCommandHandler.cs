using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.DeleteProductMedia.Commands
{
    public sealed class DeleteProductMediaCommandHandler : IRequestHandler<DeleteProductMediaCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeleteProductMediaCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteProductMediaCommand request, CancellationToken cancellationToken)
        {
            ProductMedia productMedia = (await _dbContext.ProductMedia.FindAsync(request.Id))!;
            _dbContext.ProductMedia.Remove(productMedia);

            if (await _dbContext.ProductMedia.CountAsync(x => x.ProductId == productMedia.ProductId) == 1)
            {
                Product product = (await _dbContext.Products.FindAsync(productMedia.ProductId))!;
                product.SetImage(null);
            }

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}