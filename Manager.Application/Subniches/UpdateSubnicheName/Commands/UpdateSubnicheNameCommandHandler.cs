using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.UpdateSubnicheName.Commands
{
    public sealed class UpdateSubnicheNameCommandHandler : IRequestHandler<UpdateSubnicheNameCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public UpdateSubnicheNameCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdateSubnicheNameCommand request, CancellationToken cancellationToken)
        {
            Subniche subniche = (await _dbContext.Subniches.FindAsync(request.Id))!;
            subniche.UpdateName(request.Name);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}