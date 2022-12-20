using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.DeleteSubniche.Commands
{
    public sealed class DeleteSubnicheCommandHandler : IRequestHandler<DeleteSubnicheCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeleteSubnicheCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteSubnicheCommand request, CancellationToken cancellationToken)
        {
            Subniche subniche = (await _dbContext.Subniches.FindAsync(request.Id))!;

            _dbContext.Subniches.Remove(subniche);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}