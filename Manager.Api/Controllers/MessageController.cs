using Manager.Application.Messages.GetMessage.Queries;
using Manager.Application.Messages.PostMessage.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
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



        // ----------------------------------------------------------------------- Post Message -----------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> PostMessage(PostMessageCommand postMessage)
        {
            return SetResponse(await _mediator.Send(postMessage));
        }
    }
}
