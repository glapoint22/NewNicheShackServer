using Manager.Application.Keywords.SearchKeywordGroups.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public sealed class KeywordsController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public KeywordsController(ISender mediator)
        {
            _mediator = mediator;
        }



        // ------------------------------------------------------------------------- Search Keyword Groups -----------------------------------------------------------------------
        [HttpGet]
        [Route("KeywordGroups/Search")]
        public async Task<ActionResult> SearchKeywordGroups(string searchTerm)
        {
            return SetResponse(await _mediator.Send(new SearchKeywordGroupsQuery(searchTerm)));
        }
    }
}
