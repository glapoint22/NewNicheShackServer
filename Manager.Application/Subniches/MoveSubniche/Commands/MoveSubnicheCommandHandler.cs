using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Subniches.MoveSubniche.Commands
{
    public sealed class MoveSubnicheCommandHandler : IRequestHandler<MoveSubnicheCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public MoveSubnicheCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _dbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(MoveSubnicheCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Subniche subnicheToBeMoved = (await _dbContext.Subniches.FindAsync(request.ItemToBeMovedId))!;
            subnicheToBeMoved.NicheId = request.DestinationItemId;
            await _dbContext.SaveChangesAsync();

            Website.Domain.Entities.Subniche? websiteSubniche = await _websiteDbContext.Subniches.FindAsync(request.ItemToBeMovedId);

            if (websiteSubniche != null)
            {
                // Does the niche exists on website?
                if (!await _websiteDbContext.Niches.AnyAsync(x => x.Id == request.DestinationItemId))
                {
                    Domain.Entities.Niche niche = (await _dbContext.Niches.FindAsync(request.DestinationItemId))!;

                    // Add the niche to website
                    _websiteDbContext.Niches.Add(new Website.Domain.Entities.Niche
                    {
                        Id = niche.Id,
                        Name = niche.Name,
                        UrlName = niche.UrlName
                    });
                }

                // Update the subniche
                websiteSubniche.NicheId = request.DestinationItemId;
                await _websiteDbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }
    }
}