using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.SetFilterOptionName.Commands
{
    public sealed class SetFilterOptionNameCommandHandler : IRequestHandler<SetFilterOptionNameCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public SetFilterOptionNameCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SetFilterOptionNameCommand request, CancellationToken cancellationToken)
        {
            FilterOption filterOption = (await _dbContext.FilterOptions.FindAsync(request.Id))!;

            filterOption.SetName(request.Name);

            await _dbContext.SaveChangesAsync();
            return Result.Succeeded();
        }
    }
}