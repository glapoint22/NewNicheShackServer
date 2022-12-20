using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Niches.AddNiche.Commands
{
    public sealed class AddNicheCommandHandler : IRequestHandler<AddNicheCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public AddNicheCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddNicheCommand request, CancellationToken cancellationToken)
        {
            Niche niche = Niche.Create(request.Name);

            _dbContext.Niches.Add(niche);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(niche.Id);
        }
    }
}