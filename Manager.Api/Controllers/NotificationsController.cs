using Manager.Application.Notifications.AddNoncompliantStrikeUserImage.Commands;
using Manager.Application.Notifications.AddNoncompliantStrikeUserName.Commands;
using Manager.Application.Notifications.Archive.Commands;
using Manager.Application.Notifications.DeleteNotifications.Commands;
using Manager.Application.Notifications.GetArchivedNotifications.Queries;
using Manager.Application.Notifications.GetErrorNotification.Queries;
using Manager.Application.Notifications.GetNewNotifications.Queries;
using Manager.Application.Notifications.GetNotificationCount.Queries;
using Manager.Application.Notifications.GetUserImageNotification.Queries;
using Manager.Application.Notifications.GetUserNameNotification.Queries;
using Manager.Application.Notifications.PostNote.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
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





        // ----------------------------------------------------------------------- Get User Image Notification -------------------------------------------------------------------
        [HttpGet]
        [Route("GetUserImageNotification")]
        public async Task<ActionResult> GetUserImageNotification(Guid notificationGroupId, bool isNew)
        {
            return SetResponse(await _mediator.Send(new GetUserImageNotificationQuery(notificationGroupId, isNew)));
        }




        // ------------------------------------------------------------------------------- Post Note -----------------------------------------------------------------------------
        [HttpPost]
        [Route("PostNote")]
        public async Task<ActionResult> PostNote(PostNoteCommand postNote)
        {
            return SetResponse(await _mediator.Send(postNote));
        }




        // -------------------------------------------------------------------------------- Archive ------------------------------------------------------------------------------
        [HttpPut]
        [Route("Archive")]
        public async Task<ActionResult> Archive(ArchiveCommand archive)
        {
            return SetResponse(await _mediator.Send(archive));
        }




        // --------------------------------------------------------------------- Add Noncompliant Strike User Name ---------------------------------------------------------------
        [HttpPut]
        [Route("AddNoncompliantStrikeUserName")]
        public async Task<ActionResult> AddNoncompliantStrikeUserName(AddNoncompliantStrikeUserNameCommand addNoncompliantStrikeUserName)
        {
            return SetResponse(await _mediator.Send(addNoncompliantStrikeUserName));
        }




        // -------------------------------------------------------------------- Add Noncompliant Strike User Image ---------------------------------------------------------------
        [HttpPut]
        [Route("AddNoncompliantStrikeUserImage")]
        public async Task<ActionResult> AddNoncompliantStrikeUserImage(AddNoncompliantStrikeUserImageCommand addNoncompliantStrikeUserImage)
        {
            return SetResponse(await _mediator.Send(addNoncompliantStrikeUserImage));
        }





        // --------------------------------------------------------------------------- Delete Notifications ----------------------------------------------------------------------
        [HttpDelete]
        [Route("DeleteNotifications")]
        public async Task<ActionResult> DeleteNotifications(Guid notificationGroupId, [FromQuery] List<Guid> notificationIds)
        {
            return SetResponse(await _mediator.Send(new DeleteNotificationsCommand(notificationGroupId, notificationIds)));
        }
    }
}