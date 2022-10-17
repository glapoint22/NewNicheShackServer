using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Application.Lists.ListCollection.Queries;
using Website.Application.Lists.SharedList.Queries;

namespace Website.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ListsController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public ListsController(ISender mediator)
        {
            _mediator = mediator;
        }


        // ------------------------------------------------------------------- Get List Collection ----------------------------------------------------------------------
        [HttpGet]
        [Route("ListCollection")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> GetListCollection()
        {
            return SetResponse(await _mediator.Send(new GetListCollectionQuery()));
        }






        // --------------------------------------------------------------------- Get Shared List ------------------------------------------------------------------------
        [HttpGet]
        [Route("SharedList")]
        public async Task<ActionResult> GetSharedList(string listId, string sort)
        {
            return SetResponse(await _mediator.Send(new GetSharedListQuery(listId, sort)));
        }
    }
}