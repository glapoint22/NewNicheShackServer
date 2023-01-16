using Manager.Application.Common.Interfaces;
using MediatR;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Filters.DeleteFilterOption.Commands
{
    public sealed class DeleteFilterOptionCommandHandler : IRequestHandler<DeleteFilterOptionCommand, Result>
    {
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public DeleteFilterOptionCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _managerDbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(DeleteFilterOptionCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.FilterOption managerFilterOption = (await _managerDbContext.FilterOptions.FindAsync(request.Id))!;

            _managerDbContext.FilterOptions.Remove(managerFilterOption);
            await _managerDbContext.SaveChangesAsync();


            Website.Domain.Entities.FilterOption? websiteFilterOption = await _websiteDbContext.FilterOptions.FindAsync(request.Id);

            if (websiteFilterOption != null)
            {
                _websiteDbContext.FilterOptions.Remove(websiteFilterOption);
                await _websiteDbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }
    }
}