using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Filters.SetFilterOptionName.Commands
{
    public sealed class SetFilterOptionNameCommandHandler : IRequestHandler<SetFilterOptionNameCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public SetFilterOptionNameCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _dbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(SetFilterOptionNameCommand request, CancellationToken cancellationToken)
        {
            FilterOption filterOption = (await _dbContext.FilterOptions.FindAsync(request.Id))!;

            filterOption.SetName(request.Name);

            await _dbContext.SaveChangesAsync();


            Website.Domain.Entities.FilterOption? websiteFilterOption = await _websiteDbContext.FilterOptions.FindAsync(request.Id);

            if (websiteFilterOption != null)
            {
                websiteFilterOption.Name = filterOption.Name;
                await _websiteDbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }
    }
}