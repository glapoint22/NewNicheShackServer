using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;
using Website.Application.Messages.GetMessage.Queries;

namespace Website.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public MessageController(ISender mediator)
        {
            _mediator = mediator;
        }


        // ------------------------------------------------------------------------ Get Message -----------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetMessage()
        {
            return SetResponse(await _mediator.Send(new GetMessageQuery()));
        }
    }
}
