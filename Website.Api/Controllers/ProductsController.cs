using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;
using Website.Application.Common.Interfaces;
using Website.Application.Products.GetProduct.Queries;
using Website.Application.Products.GetProperties.Queries;

namespace Website.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ProductsController : ApiControllerBase
    {
        private readonly ISender _mediator;
        private readonly ISearchSuggestionsService _searchSuggestionsService;

        public ProductsController(ISender mediator, ISearchSuggestionsService searchSuggestionsService)
        {
            _mediator = mediator;
            _searchSuggestionsService = searchSuggestionsService;
        }

        // ---------------------------------------------------------------------- Get Product --------------------------------------------------------------------------
        [HttpGet]
        [Route("GetProduct")]
        public async Task<ActionResult> GetProduct(Guid productId)
        {
            return SetResponse(await _mediator.Send(new GetProductQuery(productId)));
        }




        // --------------------------------------------------------------------- Get Properties ------------------------------------------------------------------------
        [HttpGet]
        [Route("GetProperties")]
        public async Task<ActionResult> GetProperties(Guid productId)
        {
            return SetResponse(await _mediator.Send(new GetPropertiesQuery(productId)));
        }


        // ---------------------------------------------------------------- Get Search Suggestions ---------------------------------------------------------------------
        [HttpGet]
        [Route("GetSearchSuggestions")]
        public ActionResult GetSearchSuggestions(string searchTerm, Guid nicheId)
        {
            return Ok(_searchSuggestionsService.GetSearchSuggestions(searchTerm, nicheId));
        }
    }
}