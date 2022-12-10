using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.DeleteFilterOption.Commands
{
    public sealed class DeleteFilterOptionCommandHandler : IRequestHandler<DeleteFilterOptionCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeleteFilterOptionCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteFilterOptionCommand request, CancellationToken cancellationToken)
        {
            FilterOption filterOption = (await _dbContext.FilterOptions.FindAsync(request.Id))!;

            _dbContext.FilterOptions.Remove(filterOption);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}