using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.AddFilter.Commands
{
    public sealed class AddFilterCommandHandler : IRequestHandler<AddFilterCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public AddFilterCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddFilterCommand request, CancellationToken cancellationToken)
        {
            Filter filter = Filter.Create(request.Name);

            _dbContext.Filters.Add(filter);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(filter.Id);
        }
    }
}