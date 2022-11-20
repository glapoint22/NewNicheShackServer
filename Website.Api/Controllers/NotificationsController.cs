using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;
using Website.Application.Notifications.PostMessageNotification.Commands;
using Website.Application.Notifications.PostNonAccountMessageNotification.Commands;
using Website.Application.Notifications.PostProductNotification.Commands;
using Website.Application.Notifications.PostReviewComplaintNotification.Commands;

namespace Website.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class NotificationsController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public NotificationsController(ISender mediator)
        {
            _mediator = mediator;
        }




        // ----------------------------------------------------------------- Post Non Account Message Notification ---------------------------------------------------------------
        [HttpPost]
        [Route("PostNonAccountMessage")]
        public async Task<ActionResult> PostNonAccountMessageNotification(PostNonAccountMessageNotificationCommand PostNonAccountMessageNotification)
        {
            return SetResponse(await _mediator.Send(PostNonAccountMessageNotification));
        }






        // ----------------------------------------------------------------------- Post Message Notification ---------------------------------------------------------------------
        [HttpPost]
        [Route("PostMessage")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> PostMessageNotification(PostMessageNotificationCommand PostMessageNotification)
        {
            return SetResponse(await _mediator.Send(PostMessageNotification));
        }







        // ------------------------------------------------------------------------ Post Product Notification --------------------------------------------------------------------
        [HttpPost]
        [Route("PostProductNotification")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> PostProductNotification(PostProductNotificationCommand PostProductNotification)
        {
            return SetResponse(await _mediator.Send(PostProductNotification));
        }








        // ------------------------------------------------------------------- Post Review Complaint Notification ----------------------------------------------------------------
        [HttpPost]
        [Route("PostReviewComplaintNotification")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> PostReviewComplaintNotification(PostReviewComplaintNotificationCommand PostReviewComplaintNotification)
        {
            return SetResponse(await _mediator.Send(PostReviewComplaintNotification));
        }
    }
}