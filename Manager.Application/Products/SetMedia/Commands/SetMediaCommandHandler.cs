using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetMedia.Commands
{
    public sealed class SetMediaCommandHandler : IRequestHandler<SetMediaCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetMediaCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetMediaCommand request, CancellationToken cancellationToken)
        {
            ProductMedia productMedia;

            if (request.ProductMediaId != null)
            {
                productMedia = (await _dbContext.ProductMedia.FindAsync(request.ProductMediaId))!;
                productMedia.SetMedia(request.MediaId);
            }
            else
            {
                int index = await _dbContext.ProductMedia.CountAsync(x => x.ProductId == request.ProductId);
                productMedia = ProductMedia.Create(request.ProductId, request.MediaId, index);
                _dbContext.ProductMedia.Add(productMedia);

                if (index == 0)
                {
                    Product product = (await _dbContext.Products.FindAsync(request.ProductId))!;
                    product.SetImage(request.MediaId);
                }
            }

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}