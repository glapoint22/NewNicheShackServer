using Manager.Application.QueryBuilders.GetQueryBuilderProducts.Queries;
using Manager.Application.QueryBuilders.GetQueryLists.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public sealed class QueryBuilderController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public QueryBuilderController(ISender mediator)
        {
            _mediator = mediator;
        }



        // ------------------------------------------------------------------------ Get Query Builder Products -------------------------------------------------------------------
        [HttpGet]
        [Route("GetQueryBuilderProducts")]
        public async Task<ActionResult> GetQueryBuilderProducts(string queryString)
        {
            return SetResponse(await _mediator.Send(new GetQueryBuilderProductsQuery(queryString)));
        }



        // ----------------------------------------------------------------------------- Get Query Lists -------------------------------------------------------------------------
        [HttpGet]
        [Route("GetQueryLists")]
        public async Task<ActionResult> GetQueryLists()
        {
            return SetResponse(await _mediator.Send(new GetQueryListsQuery()));
        }
    }
}