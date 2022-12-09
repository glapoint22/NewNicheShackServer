using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetProductMedia.Commands
{
    public sealed class SetProductMediaCommandHandler : IRequestHandler<SetProductMediaCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetProductMediaCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetProductMediaCommand request, CancellationToken cancellationToken)
        {
            Product product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.ProductMedia)
                .SingleAsync();

            ProductMedia productMedia = product.SetProductMedia(request.ProductMediaId, request.MediaId);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded(productMedia.Id);
        }
    }
}