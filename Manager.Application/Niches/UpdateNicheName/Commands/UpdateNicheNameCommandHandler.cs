using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Niches.UpdateNicheName.Commands
{
    public sealed class UpdateNicheNameCommandHandler : IRequestHandler<UpdateNicheNameCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public UpdateNicheNameCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdateNicheNameCommand request, CancellationToken cancellationToken)
        {
            Niche niche = (await _dbContext.Niches.FindAsync(request.Id))!;
            niche.UpdateName(request.Name);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}