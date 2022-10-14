using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website.Application.Account.IsDuplicateEmail.Queries;
using Website.Application.Lists.List.Queries;

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



        // --------------------------------------------------------------------- Get Shared List ------------------------------------------------------------------------
        [HttpGet]
        [Route("SharedList")]
        public async Task<ActionResult> GetSharedList(string listId, string sort)
        {
            return Ok(await _mediator.Send(new GetSharedListQuery(listId, sort)));
        }
    }
}