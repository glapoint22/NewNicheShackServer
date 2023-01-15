using Manager.Application._Publish.GetPublishItems.Queries;
using Manager.Application._Publish.PublishProduct.Commands;
using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
using Manager.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public sealed class PublishController : ApiControllerBase
    {
        private readonly ISender _mediator;
        private readonly IManagerDbContext _dbContext;

        public PublishController(ISender mediator, IManagerDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }



        // ---------------------------------------------------------------------------- Get Publish Items ------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetPublishItems()
        {
            return SetResponse(await _mediator.Send(new GetPublishItemsQuery()));
        }






        // ------------------------------------------------------------------------------ Publish Product ------------------------------------------------------------------------
        [HttpGet]
        [Route("PublishProduct")]
        public async Task<ActionResult> PublishProduct(Guid productId)
        {
            Product product = await _dbContext.Products
                .AsSplitQuery()
                .Where(x => x.Id == productId)
                .Include(x => x.Subniche)
                    .ThenInclude(x => x.Niche)
                .Include(x => x.ProductFilters)
                    .ThenInclude(x => x.FilterOption)
                .Include(x => x.ProductKeywords)
                .Include(x => x.ProductMedia)
                    .ThenInclude(x => x.Media)
                .Include(x => x.ProductPrices)
                .Include(x => x.ProductsInProductGroup)
                .Include(x => x.PricePoints)
                    .ThenInclude(x => x.ProductPrice)
                .Include(x => x.Subproducts)
                .SingleAsync();

            return SetResponse(await _mediator.Send(new PublishProductCommand(product)));
        }
    }
}
