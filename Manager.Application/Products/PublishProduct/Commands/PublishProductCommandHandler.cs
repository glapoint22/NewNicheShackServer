using Manager.Application.Common.Interfaces;
using Manager.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Classes;
using Website.Application.Common.Interfaces;

namespace Manager.Application.Products.PublishProduct.Commands
{
    public sealed class PublishProductCommandHandler : IRequestHandler<PublishProductCommand, Result>
    {
        private readonly IManagerDbContext _managerDbContext;
        private readonly IWebsiteDbContext _websiteDbContext;

        public PublishProductCommandHandler(IManagerDbContext managerDbContext, IWebsiteDbContext websiteDbContext)
        {
            _managerDbContext = managerDbContext;
            _websiteDbContext = websiteDbContext;
        }

        public async Task<Result> Handle(PublishProductCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Publish publish = await _managerDbContext.Publishes
                .Where(x => x.ProductId == request.ProductId)
                .SingleAsync();

            if (publish.PublishStatus == PublishStatus.New)
            {
                await PostProduct(request.ProductId);
            }
            else
            {
                await UpdateProduct(request.ProductId);
            }

            return Result.Succeeded();
        }





        private async Task PostProduct(Guid productId)
        {
            Domain.Entities.Product managerProduct = await _managerDbContext.Products
                .AsSplitQuery()
                .Where(x => x.Id == productId)
                .Include(x => x.ProductFilters)
                    .ThenInclude(x => x.FilterOption)
                .Include(x => x.ProductKeywords)
                .Include(x => x.ProductMedia)
                .Include(x => x.ProductPrices)
                .Include(x => x.ProductsInProductGroup)
                .Include(x => x.PricePoints)
                .Include(x => x.Subproducts)
                .SingleAsync();


            var websiteFilterIds = await _websiteDbContext.Filters
                .Where(x => managerProduct.ProductFilters
                    .Select(z => z.FilterOption.FilterId)
                    .Contains(x.Id))
                .Select(z => z.Id)
                .ToListAsync();

            List<Guid> managerFilterIds = managerProduct.ProductFilters
                .Where(x => !websiteFilterIds
                    .Contains(x.FilterOption.FilterId))
                .Select(x => x.FilterOption.FilterId)
                .ToList();

            if (managerFilterIds.Count > 0)
            {
                List<Website.Domain.Entities.Filter> filters = await _managerDbContext.Filters
                .Where(x => managerFilterIds.Contains(x.Id))
                .Select(x => new Website.Domain.Entities.Filter
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

                _websiteDbContext.Filters.AddRange(filters);
            }




        }




        private Task UpdateProduct(Guid productId)
        {
            throw new NotImplementedException();
        }


    }
}