using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Events;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.AddSubniche.Commands
{
    public sealed class AddSubnicheCommandHandler : IRequestHandler<AddSubnicheCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public AddSubnicheCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddSubnicheCommand request, CancellationToken cancellationToken)
        {
            Subniche subniche = Subniche.Create(request.Id, request.Name);

            _dbContext.Subniches.Add(subniche);

            subniche.AddDomainEvent(new HierarchyItemCreatedEvent(request.Name));
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(subniche.Id);
        }
    }
}