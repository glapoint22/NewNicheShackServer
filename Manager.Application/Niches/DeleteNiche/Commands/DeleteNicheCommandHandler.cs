using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Niches.DeleteNiche.Commands
{
    public sealed class DeleteNicheCommandHandler : IRequestHandler<DeleteNicheCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeleteNicheCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteNicheCommand request, CancellationToken cancellationToken)
        {
            Niche niche = (await _dbContext.Niches.FindAsync(request.Id))!;

            _dbContext.Niches.Remove(niche);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}