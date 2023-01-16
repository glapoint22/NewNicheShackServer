using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.MoveSubniche.Commands
{
    public sealed class MoveSubnicheCommandHandler : IRequestHandler<MoveSubnicheCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public MoveSubnicheCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(MoveSubnicheCommand request, CancellationToken cancellationToken)
        {
            Subniche subnicheToBeMoved = (await _dbContext.Subniches.FindAsync(request.ItemToBeMovedId))!;

            subnicheToBeMoved.NicheId = request.DestinationItemId;

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}