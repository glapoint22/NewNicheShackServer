using Manager.Application.Products.SearchProducts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public class ProductsController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public ProductsController(ISender mediator)
        {
            _mediator = mediator;
        }


        // ------------------------------------------------------------------------------ Search Products ------------------------------------------------------------------------
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult> SearchProducts(string searchTerm)
        {
            return SetResponse(await _mediator.Send(new SearchProductsQuery(searchTerm)));
        }
    }
}