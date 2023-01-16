using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Filters.SetFilterName.Commands
{
    public sealed class SetFilterNameCommandHandler : IRequestHandler<SetFilterNameCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public SetFilterNameCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _dbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(SetFilterNameCommand request, CancellationToken cancellationToken)
        {
            Filter filter = (await _dbContext.Filters.FindAsync(request.Id))!;

            filter.SetName(request.Name);

            await _dbContext.SaveChangesAsync();


            Website.Domain.Entities.Filter? websiteFilter = await _websiteDbContext.Filters.FindAsync(request.Id);

            if (websiteFilter != null)
            {
                websiteFilter.Name = filter.Name;
                await _websiteDbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }
    }
}