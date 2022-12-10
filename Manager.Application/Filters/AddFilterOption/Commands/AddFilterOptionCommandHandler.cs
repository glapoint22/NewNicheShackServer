using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.AddFilterOption.Commands
{
    public sealed class AddFilterOptionCommandHandler : IRequestHandler<AddFilterOptionCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;

        public AddFilterOptionCommandHandler(IManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(AddFilterOptionCommand request, CancellationToken cancellationToken)
        {
            FilterOption filterOption = FilterOption.Create(request.Id, request.Name);

            _dbContext.FilterOptions.Add(filterOption);
            await _dbContext.SaveChangesAsync();

            return Result.Succeeded(filterOption.Id);
        }
    }
}