using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Niches.DisableEnableNiche.Commands
{
    public sealed class DisableEnableNicheCommandHandler : IRequestHandler<DisableEnableNicheCommand, Result>
    {
        private bool disabledState;
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public DisableEnableNicheCommandHandler(IManagerDbContext managerDbContext, IWebsiteDbContext websiteDbContext)
        {
            _managerDbContext = managerDbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(DisableEnableNicheCommand request, CancellationToken cancellationToken)
        {
            //-----------------------------------Manager-----------------------------------\\

            // Find the selected niche
            Domain.Entities.Niche managerNiche = (await _managerDbContext.Niches.FindAsync(request.NicheId))!;

            // If the selected niche is found
            if (managerNiche != null)
            {
                // Update the disabled state of the selected niche
                disabledState = managerNiche.Disabled = !managerNiche.Disabled;

                // Get all of the subniches that fall under the selected niche
                var managerSubniches = await _managerDbContext.Subniches
                    .Where(x => x.NicheId == request.NicheId)
                    .ToListAsync();

                // Update the disabled state of all the subniches that fall under the selected niche
                managerSubniches.ForEach(x => x.Disabled = disabledState);

                // Get the ids of all the subniches that fall under the selected niche
                var managerSubnicheIds = managerSubniches.Select(subniche => subniche.Id).ToList();

                // Update the disabled state of every product that falls under each subniche 
                await _managerDbContext.Products
                    .Where(x => managerSubnicheIds.Contains(x.SubnicheId))
                    .ForEachAsync(product => product.Disabled = disabledState);

                await _managerDbContext.SaveChangesAsync();
            }





            //-----------------------------------Website-----------------------------------\\

            // Find the selected niche
            Website.Domain.Entities.Niche websiteNiche = (await _websiteDbContext.Niches.FindAsync(request.NicheId))!;

            // If the selected niche is found
            if (websiteNiche != null)
            {
                // Update the disabled state of the selected niche
                websiteNiche.Disabled = disabledState;

                // Get all of the subniches that fall under the selected niche
                var websiteSubniches = await _websiteDbContext.Subniches
                    .Where(x => x.NicheId == request.NicheId)
                    .ToListAsync();

                // Update the disabled state of all the subniches that fall under the selected niche
                websiteSubniches.ForEach(x => x.Disabled = disabledState);

                // Get the ids of all the subniches that fall under the selected niche
                var websiteSubnicheIds = websiteSubniches.Select(subniche => subniche.Id).ToList();

                // Update the disabled state of every product that falls under each subniche
                await _websiteDbContext.Products
                    .Where(x => websiteSubnicheIds.Contains(x.SubnicheId))
                    .ForEachAsync(product => product.Disabled = disabledState);

                await _websiteDbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }
    }
}