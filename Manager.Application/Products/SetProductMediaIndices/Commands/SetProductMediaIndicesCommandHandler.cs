using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetProductMediaIndices.Commands
{
    public sealed class SetProductMediaIndicesCommandHandler : IRequestHandler<SetProductMediaIndicesCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetProductMediaIndicesCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetProductMediaIndicesCommand request, CancellationToken cancellationToken)
        {
            Product product = await _dbContext.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.ProductMedia)
                    .ThenInclude(x => x.Media)
                .SingleAsync();

            product.SetProductMediaIndices(request.ProductMedia);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}