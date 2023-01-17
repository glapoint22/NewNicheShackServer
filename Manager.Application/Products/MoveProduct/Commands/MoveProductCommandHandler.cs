using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Products.MoveProduct.Commands
{
    public sealed class MoveProductCommandHandler : IRequestHandler<MoveProductCommand, Result>
    {
        private readonly IManagerDbContext _dbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public MoveProductCommandHandler(IManagerDbContext dbContext, IWebsiteDbContext websiteDbContext)
        {
            _dbContext = dbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(MoveProductCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product productToBeMoved = (await _dbContext.Products.FindAsync(request.ItemToBeMovedId))!;
            productToBeMoved.SubnicheId = request.DestinationItemId;
            await _dbContext.SaveChangesAsync();

            Website.Domain.Entities.Product? websiteProduct = await _websiteDbContext.Products.FindAsync(request.ItemToBeMovedId);

            // If the product exists on website
            if (websiteProduct != null)
            {
                // Does the subniche exists on website?
                if (!await _websiteDbContext.Subniches.AnyAsync(x => x.Id == request.DestinationItemId))
                {
                    Domain.Entities.Subniche subniche = (await _dbContext.Subniches.FindAsync(request.DestinationItemId))!;

                    // Does the niche exists on website?
                    if (!await _websiteDbContext.Niches.AnyAsync(x => x.Id == subniche.NicheId))
                    {
                        Domain.Entities.Niche niche = (await _dbContext.Niches.FindAsync(subniche.NicheId))!;

                        // Add the niche to website
                        _websiteDbContext.Niches.Add(new Website.Domain.Entities.Niche
                        {
                            Id = niche.Id,
                            Name = niche.Name,
                            UrlName = niche.UrlName
                        });
                    }

                    // Add the subniche to website
                    _websiteDbContext.Subniches.Add(new Website.Domain.Entities.Subniche
                    {
                        Id = subniche.Id,
                        NicheId = subniche.NicheId,
                        Name = subniche.Name,
                        UrlName = subniche.UrlName
                    });
                }

                // Update the product
                websiteProduct.SubnicheId = request.DestinationItemId;
                await _websiteDbContext.SaveChangesAsync();
            }

            return Result.Succeeded();
        }
    }
}