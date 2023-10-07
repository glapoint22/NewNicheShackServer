using Manager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;
using Website.Domain.Entities;

namespace Manager.Application._Publish.PublishPriceRanges.Commands
{
    public sealed class PublishPriceRangesCommandHandler : IRequestHandler<PublishPriceRangesCommand, Result>
    {
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public PublishPriceRangesCommandHandler(IManagerDbContext managerDbContext, IWebsiteDbContext websiteDbContext)
        {
            _managerDbContext = managerDbContext;
            _websiteDbContext = websiteDbContext;
        }


        public async Task<Result> Handle(PublishPriceRangesCommand request, CancellationToken cancellationToken)
        {
            List<PriceRange> priceRanges = await _managerDbContext.PriceRanges
                .Select(x => new PriceRange
                {
                    Label = x.Label,
                    Min = x.Min,
                    Max = x.Max
                })
                .ToListAsync();

            _websiteDbContext.PriceRanges.AddRange(priceRanges);
            await _websiteDbContext.SaveChangesAsync();

            return Result.Succeeded();
        }
    }
}