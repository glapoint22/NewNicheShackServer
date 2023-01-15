using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using MediatR;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Niches.UpdateNicheName.Commands
{
    public sealed class UpdateNicheNameCommandHandler : IRequestHandler<UpdateNicheNameCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public UpdateNicheNameCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _dbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(UpdateNicheNameCommand request, CancellationToken cancellationToken)
        {
            Niche niche = (await _dbContext.Niches.FindAsync(request.Id))!;
            niche.UpdateName(request.Name);

            await _dbContext.SaveChangesAsync();


            Website.Domain.Entities.Niche? websiteNiche = await _websiteDbContext.Niches.FindAsync(request.Id);

            if (websiteNiche != null)
            {
                websiteNiche.Name = niche.Name;
                websiteNiche.UrlName = niche.UrlName;
                await _websiteDbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }
    }
}