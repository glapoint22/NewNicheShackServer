using Manager.Application.Common.Interfaces;
using MediatR;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Subniches.DeleteSubniche.Commands
{
    public sealed class DeleteSubnicheCommandHandler : IRequestHandler<DeleteSubnicheCommand, Result>
    {
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public DeleteSubnicheCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _managerDbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(DeleteSubnicheCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Subniche managerSubniche = (await _managerDbContext.Subniches.FindAsync(request.Id))!;

            _managerDbContext.Subniches.Remove(managerSubniche);
            await _managerDbContext.SaveChangesAsync();

            
            
            Website.Domain.Entities.Subniche? websiteSubniche = await _websiteDbContext.Subniches.FindAsync(request.Id);

            if (websiteSubniche != null)
            {
                _websiteDbContext.Subniches.Remove(websiteSubniche);
                await _websiteDbContext.SaveChangesAsync();
            }


            return Result.Succeeded();
        }
    }
}