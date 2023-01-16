using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Subniches.UpdateSubnicheName.Commands
{
    public sealed class UpdateSubnicheNameCommandHandler : IRequestHandler<UpdateSubnicheNameCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public UpdateSubnicheNameCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _dbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(UpdateSubnicheNameCommand request, CancellationToken cancellationToken)
        {
            Subniche subniche = (await _dbContext.Subniches.FindAsync(request.Id))!;
            subniche.UpdateName(request.Name);

            await _dbContext.SaveChangesAsync();


            Website.Domain.Entities.Subniche? websiteSubniche = await _websiteDbContext.Subniches.FindAsync(request.Id);

            if (websiteSubniche != null)
            {
                websiteSubniche.Name = subniche.Name;
                websiteSubniche.UrlName = subniche.UrlName;
                await _websiteDbContext.SaveChangesAsync();
            }


            return Result.Succeeded();
        }
    }
}