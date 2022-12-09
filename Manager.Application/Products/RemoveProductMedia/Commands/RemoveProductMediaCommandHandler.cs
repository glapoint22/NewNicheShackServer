using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.RemoveProductMedia.Commands
{
    public sealed class RemoveProductMediaCommandHandler : IRequestHandler<RemoveProductMediaCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public RemoveProductMediaCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(RemoveProductMediaCommand request, CancellationToken cancellationToken)
        {
            Product product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.ProductMedia)
                    .ThenInclude(x => x.Media)
                .SingleAsync();

            product.RemoveProductMedia(request.ProductMediaId);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}