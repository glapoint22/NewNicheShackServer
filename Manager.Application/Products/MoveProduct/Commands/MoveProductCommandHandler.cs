using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.MoveProduct.Commands
{
    public sealed class MoveProductCommandHandler : IRequestHandler<MoveProductCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public MoveProductCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(MoveProductCommand request, CancellationToken cancellationToken)
        {
            Product productToBeMoved = (await _dbContext.Products.FindAsync(request.ItemToBeMovedId))!;

            productToBeMoved.SubnicheId = request.DestinationItemId;

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}