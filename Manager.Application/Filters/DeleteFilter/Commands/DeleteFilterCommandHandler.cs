using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.DeleteFilter.Commands
{
    public sealed class DeleteFilterCommandHandler : IRequestHandler<DeleteFilterCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public DeleteFilterCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteFilterCommand request, CancellationToken cancellationToken)
        {
            Filter filter = (await _dbContext.Filters.FindAsync(request.Id))!;

            _dbContext.Filters.Remove(filter);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}