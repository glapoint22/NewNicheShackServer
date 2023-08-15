using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Subniches.DisableEnableSubniche.Commands
{
    public sealed class DisableEnableSubnicheCommandHandler : IRequestHandler<DisableEnableSubnicheCommand, Result>
    {
        private bool disabledState;
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public DisableEnableSubnicheCommandHandler(IManagerDbContext managerDbContext, IWebsiteDbContext websiteDbContext)
        {
            _managerDbContext = managerDbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(DisableEnableSubnicheCommand request, CancellationToken cancellationToken)
        {

            //-----------------------------------Manager-----------------------------------\\

            // Find the selected subniche
            Domain.Entities.Subniche managerSubniche = (await _managerDbContext.Subniches.FindAsync(request.SubnicheId))!;

            // If the selected subniche is found
            if (managerSubniche != null)
            {
                // Update the disabled state of the selected subniche
                disabledState = managerSubniche.Disabled = !managerSubniche.Disabled;

                // Update the disabled state of all the products that fall under the selected subniche
                await _managerDbContext.Products
                    .Where(x => x.SubnicheId == request.SubnicheId)
                    .ForEachAsync(product => product.Disabled = disabledState);

                // Get the parent of the selected subniche
                var managerNiche = await _managerDbContext.Niches.Where(x => x.Id == managerSubniche.NicheId).SingleAsync();

                // Find all the siblings of the selected subniche (Not including the selected subniche)
                var managerSubnicheSiblings = await _managerDbContext.Subniches
                    .Where(x => x.NicheId == managerSubniche.NicheId && x.Id != managerSubniche.Id)
                    .ToListAsync();


                // Check to see if all the siblings of the selected subniche are disabled (Not including the selected subniche)
                bool managerSiblingsDisabled = managerSubnicheSiblings.All(x => x.Disabled);


                // If all the siblings of the selected subniche are disabled and the disabled state of the parent does (NOT) match the disabled state of the selected subniche
                if (managerSiblingsDisabled && managerNiche.Disabled != disabledState)
                {
                    // Set the disabled state of the parent the same as the disabled state of the selected subniche
                    managerNiche.Disabled = disabledState;
                }
                await _managerDbContext.SaveChangesAsync();
            }






            //-----------------------------------Website-----------------------------------\\

            // Find the selected subniche
            Website.Domain.Entities.Subniche websiteSubniche = (await _websiteDbContext.Subniches.FindAsync(request.SubnicheId))!;

            // If the selected subniche is found
            if (websiteSubniche != null)
            {
                // Update the disabled state of the selected subniche
                websiteSubniche.Disabled = disabledState;

                // Update the disabled state of all the products that fall under the selected subniche
                await _websiteDbContext.Products
                    .Where(x => x.SubnicheId == request.SubnicheId)
                    .ForEachAsync(product => product.Disabled = disabledState);

                // Get the parent of the selected subniche
                var websiteNiche = await _websiteDbContext.Niches.Where(x => x.Id == websiteSubniche.NicheId).SingleAsync();

                // Find all the siblings of the selected subniche (Not including the selected subniche)
                var websiteSubnicheSiblings = await _websiteDbContext.Subniches
                    .Where(x => x.NicheId == websiteSubniche.NicheId && x.Id != websiteSubniche.Id)
                    .ToListAsync();


                // Check to see if all the siblings of the selected subniche are disabled (Not including the selected subniche)
                bool websiteSiblingsDisabled = websiteSubnicheSiblings.All(x => x.Disabled);


                // If all the siblings of the selected subniche are disabled and the disabled state of the parent does (NOT) match the disabled state of the selected subniche
                if (websiteSiblingsDisabled && websiteNiche.Disabled != disabledState)
                {
                    // Set the disabled state of the parent the same as the disabled state of the selected subniche
                    websiteNiche.Disabled = disabledState;
                }
                await _websiteDbContext.SaveChangesAsync();
            }



            return Result.Succeeded();
        }
    }
}