using Manager.Application.Notifications.GetArchivedNotifications.Queries;
using Manager.Application.Notifications.GetErrorNotification.Queries;
using Manager.Application.Notifications.GetNewNotifications.Queries;
using Manager.Application.Notifications.GetNotificationCount.Queries;
using Manager.Application.Notifications.GetUserNameNotification.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
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


        // ------------------------------------------------------------------------- Get New Notifications -----------------------------------------------------------------------
        [HttpGet]
        [Route("GetNewNotifications")]
        public async Task<ActionResult> GetNewNotifications(int currentCount)
        {
            int newCount = await _mediator.Send(new GetNotificationCountQuery());

            if (newCount != currentCount)
            {
                return SetResponse(await _mediator.Send(new GetNewNotificationsQuery()));
            }

            return Ok();
        }



        // ----------------------------------------------------------------------- Get Archived Notifications --------------------------------------------------------------------
        [HttpGet]
        [Route("GetArchivedNotifications")]
        public async Task<ActionResult> GetArchivedNotifications()
        {
            return SetResponse(await _mediator.Send(new GetArchivedNotificationsQuery()));
        }





        // ------------------------------------------------------------------------- Get Error Notification ----------------------------------------------------------------------
        [HttpGet]
        [Route("GetErrorNotification")]
        public async Task<ActionResult> GetErrorNotification(Guid notificationGroupId)
        {
            return SetResponse(await _mediator.Send(new GetErrorNotificationQuery(notificationGroupId)));
        }




        // ----------------------------------------------------------------------- Get User Name Notification --------------------------------------------------------------------
        [HttpGet]
        [Route("GetUserNameNotification")]
        public async Task<ActionResult> GetUserNameNotification(Guid notificationGroupId, bool isNew)
        {
            return SetResponse(await _mediator.Send(new GetUserNameNotificationQuery(notificationGroupId, isNew)));
        }
    }
}