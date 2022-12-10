using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.SetFilterName.Commands
{
    public sealed class SetFilterNameCommandHandler : IRequestHandler<SetFilterNameCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetFilterNameCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetFilterNameCommand request, CancellationToken cancellationToken)
        {
            Filter filter = (await _dbContext.Filters.FindAsync(request.Id))!;

            filter.SetName(request.Name);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}