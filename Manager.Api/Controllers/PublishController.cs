using Manager.Application._Publish.GetPublishItems.Queries;
using Manager.Application._Publish.PublishEmail.Commands;
using Manager.Application._Publish.PublishPage.Commands;
using Manager.Application._Publish.PublishPriceRanges.Commands;
using Manager.Application._Publish.PublishProduct.Commands;
using Manager.Application._Publish.PublishStars.Commands;
using Manager.Application.Common.Interfaces;
using Manager.Domain.Entities;
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
        private readonly IAuthService _authService;

        public PublishController(ISender mediator, IManagerDbContext dbContext, IAuthService authService)
        {
            _mediator = mediator;
            _dbContext = dbContext;
            _authService = authService;
        }



        // ---------------------------------------------------------------------------- Get Publish Items ------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetPublishItems()
        {
            return SetResponse(await _mediator.Send(new GetPublishItemsQuery()));
        }







        // ------------------------------------------------------------------------------- Publish Email -------------------------------------------------------------------------
        [HttpGet]
        [Route("PublishEmail")]
        public async Task<ActionResult> PublishEmail(Guid emailId)
        {
            return SetResponse(await _mediator.Send(new PublishEmailCommand(emailId)));
        }















        // -------------------------------------------------------------------------------- Publish Page -------------------------------------------------------------------------
        [HttpGet]
        [Route("PublishPage")]
        public async Task<ActionResult> PublishPage(Guid pageId)
        {
            return SetResponse(await _mediator.Send(new PublishPageCommand(pageId)));
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





        // -------------------------------------------------------------------------------- Publish All -------------------------------------------------------------------------
        [HttpGet]
        [Route("PublishAll")]
        public async Task<ActionResult> PublishAll()
        {
            // Products
            var productIds = await _dbContext.Products.Select(x => x.Id).ToListAsync();

            foreach (var productId in productIds)
            {
                _dbContext.PublishItems.Add(PublishItem.AddProduct(productId, _authService.GetUserIdFromClaims(), Domain.Enums.PublishStatus.New));

                await _dbContext.SaveChangesAsync();

                await PublishProduct(productId);
            }


            // Pages
            var pageIds = await _dbContext.Pages.Select(x => x.Id).ToListAsync();

            foreach (var pageId in pageIds)
            {
                _dbContext.PublishItems.Add(PublishItem.AddPage(pageId, _authService.GetUserIdFromClaims(), Domain.Enums.PublishStatus.New));
                await _dbContext.SaveChangesAsync();

                await PublishPage(pageId);
            }


            // Emails
            var emailIds = await _dbContext.Emails.Select(x => x.Id).ToListAsync();

            foreach (var emailId in emailIds)
            {
                _dbContext.PublishItems.Add(PublishItem.AddEmail(emailId, _authService.GetUserIdFromClaims(), Domain.Enums.PublishStatus.New));
                await _dbContext.SaveChangesAsync();

                await PublishEmail(emailId);
            }

            await _mediator.Send(new PublishStarsCommand());


            // Price Ranges
            await _mediator.Send(new PublishPriceRangesCommand());

            return Ok();
        }
    }
}