using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Application.Notifications.PostNotification.Commands;

namespace Website.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public sealed class NotificationsController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public NotificationsController(ISender mediator)
        {
            _mediator = mediator;
        }


        // --------------------------------------------------------------------------- Post Notification -------------------------------------------------------------------------
        [HttpPost]
        [Route("PostNotification")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> PostNotification(PostNotificationCommand postNotification)
        {
            return SetResponse(await _mediator.Send(postNotification));
        }




        // ----------------------------------------------------------------------------- Post Message ----------------------------------------------------------------------------
        [HttpPost]
        [Route("PostMessage")]
        public async Task<ActionResult> PostMessage(PostNotificationCommand postNotification)
        {
            return SetResponse(await _mediator.Send(postNotification));
        }
    }
}