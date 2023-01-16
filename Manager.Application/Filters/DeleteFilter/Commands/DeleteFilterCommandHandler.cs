using Manager.Application.Common.Interfaces;
using MediatR;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Filters.DeleteFilter.Commands
{
    public sealed class DeleteFilterCommandHandler : IRequestHandler<DeleteFilterCommand, Result>
    {
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public DeleteFilterCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _managerDbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(DeleteFilterCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Filter managerFilter = (await _managerDbContext.Filters.FindAsync(request.Id))!;

            _managerDbContext.Filters.Remove(managerFilter);
            await _managerDbContext.SaveChangesAsync();



            Website.Domain.Entities.Filter? websiteFilter = await _websiteDbContext.Filters.FindAsync(request.Id);

            if (websiteFilter != null)
            {
                _websiteDbContext.Filters.Remove(websiteFilter);
                await _websiteDbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }
    }
}