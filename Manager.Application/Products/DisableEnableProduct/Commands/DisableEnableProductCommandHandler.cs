using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Products.DisableEnableProduct.Commands
{
    public sealed class DisableEnableProductCommandHandler : IRequestHandler<DisableEnableProductCommand, Result>
    {
        private bool disabledState;
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public DisableEnableProductCommandHandler(IManagerDbContext managerDbContext, IWebsiteDbContext websiteDbContext)
        {
            _managerDbContext = managerDbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(DisableEnableProductCommand request, CancellationToken cancellationToken)
        {

            //-----------------------------------Manager-----------------------------------\\

            // Find the selected product
            Domain.Entities.Product managerProduct = await _managerDbContext.Products.Where(x => x.Id == request.ProductId).SingleAsync();

            // If the selected product is found
            if (managerProduct != null)
            {
                // Update the disabled state of the selected product
                disabledState = managerProduct.Disabled = !managerProduct.Disabled;

                // Get the parent of the selected product
                var managerSubniche = await _managerDbContext.Subniches.Where(x => x.Id == managerProduct.SubnicheId).SingleAsync();

                // Find all the siblings of the selected product (Not including the selected product)
                var managerProductSiblings = await _managerDbContext.Products
                    .Where(x => x.SubnicheId == managerProduct.SubnicheId && x.Id != managerProduct.Id)
                    .ToListAsync();

                // Check to see if all the siblings of the selected product are disabled (Not including the selected product)
                bool managerProductSiblingsDisabled = managerProductSiblings.All(x => x.Disabled);

                // If all the siblings of the selected product are disabled and the disabled state of the parent does (NOT) match the disabled state of the selected product
                if (managerProductSiblingsDisabled && managerSubniche.Disabled != disabledState)
                {
                    // Set the disabled state of the parent the same as the disabled state of the selected product
                    managerSubniche.Disabled = disabledState;
                } 



                // Get the parent of the parent subniche
                var managerNiche = await _managerDbContext.Niches.Where(x => x.Id == managerSubniche.NicheId).SingleAsync();

                // Find all the siblings of the parent subniche (Not including the parent subniche)
                var managerSubnicheSiblings = await _managerDbContext.Subniches
                    .Where(x => x.NicheId == managerSubniche.NicheId && x.Id != managerSubniche.Id)
                    .ToListAsync();

                // Check to see if all the siblings of the parent subniche are disabled (Not including the parent subniche)
                bool managerSubnicheSiblingsDisabled = managerSubnicheSiblings.All(x => x.Disabled);

                // If all the siblings of the parent subniche are disabled and the disabled state of its parent does (NOT) match the disabled state of the parent subniche
                if (managerSubnicheSiblingsDisabled && managerNiche.Disabled != managerSubniche.Disabled)
                {
                    // Set the disabled state of the parent the same as the disabled state of the parent subniche
                    managerNiche.Disabled = managerSubniche.Disabled;
                }

                await _managerDbContext.SaveChangesAsync();
            }


            //-----------------------------------Website-----------------------------------\\

            // Find the selected product
            Website.Domain.Entities.Product websiteProduct = await _websiteDbContext.Products.Where(x => x.Id == request.ProductId).SingleAsync();

            // If the selected product is found
            if (websiteProduct != null)
            {
                // Update the disabled state of the selected product
                websiteProduct.Disabled = disabledState;

                // Get the parent of the selected product
                var websiteSubniche = await _websiteDbContext.Subniches.Where(x => x.Id == websiteProduct.SubnicheId).SingleAsync();

                // Find all the siblings of the selected product (Not including the selected product)
                var websiteProductSiblings = await _websiteDbContext.Products
                    .Where(x => x.SubnicheId == websiteProduct.SubnicheId && x.Id != websiteProduct.Id)
                    .ToListAsync();

                // Check to see if all the siblings of the selected product are disabled (Not including the selected product)
                bool websiteProductSiblingsDisabled = websiteProductSiblings.All(x => x.Disabled);

                // If all the siblings of the selected product are disabled and the disabled state of the parent does (NOT) match the disabled state of the selected product
                if (websiteProductSiblingsDisabled && websiteSubniche.Disabled != disabledState)
                {
                    // Set the disabled state of the parent the same as the disabled state of the selected product
                    websiteSubniche.Disabled = disabledState;
                }



                // Get the parent of the parent subniche
                var websiteNiche = await _websiteDbContext.Niches.Where(x => x.Id == websiteSubniche.NicheId).SingleAsync();

                // Find all the siblings of the parent subniche (Not including the parent subniche)
                var websiteSubnicheSiblings = await _websiteDbContext.Subniches
                    .Where(x => x.NicheId == websiteSubniche.NicheId && x.Id != websiteSubniche.Id)
                    .ToListAsync();

                // Check to see if all the siblings of the parent subniche are disabled (Not including the parent subniche)
                bool websiteSubnicheSiblingsDisabled = websiteSubnicheSiblings.All(x => x.Disabled);

                // If all the siblings of the parent subniche are disabled and the disabled state of its parent does (NOT) match the disabled state of the parent subniche
                if (websiteSubnicheSiblingsDisabled && websiteNiche.Disabled != websiteSubniche.Disabled)
                {
                    // Set the disabled state of the parent the same as the disabled state of the parent subniche
                    websiteNiche.Disabled = websiteSubniche.Disabled;
                }

                await _websiteDbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }
    }
}