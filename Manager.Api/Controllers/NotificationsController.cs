using Manager.Application.Notifications.AddNoncompliantStrikeList.Commands;
using Manager.Application.Notifications.AddNoncompliantStrikeUserImage.Commands;
using Manager.Application.Notifications.AddNoncompliantStrikeUserName.Commands;
using Manager.Application.Notifications.Archive.Commands;
using Manager.Application.Notifications.DeleteNotifications.Commands;
using Manager.Application.Notifications.GetArchivedNotifications.Queries;
using Manager.Application.Notifications.GetErrorNotification.Queries;
using Manager.Application.Notifications.GetListNotification.Queries;
using Manager.Application.Notifications.GetMessageNotification.Queries;
using Manager.Application.Notifications.GetNewNotifications.Queries;
using Manager.Application.Notifications.GetNotificationCount.Queries;
using Manager.Application.Notifications.GetProductNotification.Queries;
using Manager.Application.Notifications.GetReviewComplaintNotification.Queries;
using Manager.Application.Notifications.GetReviewNotification.Queries;
using Manager.Application.Notifications.GetUserImageNotification.Queries;
using Manager.Application.Notifications.GetUserNameNotification.Queries;
using Manager.Application.Notifications.PostNote.Commands;
using Manager.Application.Notifications.RemoveReview.Commands;
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




        // ---------------------------------------------------------------------- Add Noncompliant Strike List -------------------------------------------------------------------
        [HttpPut]
        [Route("AddNoncompliantStrikeList")]
        public async Task<ActionResult> AddNoncompliantStrikeList(AddNoncompliantStrikeListCommand addNoncompliantStrikeList)
        {
            return SetResponse(await _mediator.Send(addNoncompliantStrikeList));
        }






        // -------------------------------------------------------------------- Add Noncompliant Strike User Image ---------------------------------------------------------------
        [HttpPut]
        [Route("AddNoncompliantStrikeUserImage")]
        public async Task<ActionResult> AddNoncompliantStrikeUserImage(AddNoncompliantStrikeUserImageCommand addNoncompliantStrikeUserImage)
        {
            return SetResponse(await _mediator.Send(addNoncompliantStrikeUserImage));
        }








        // --------------------------------------------------------------------- Add Noncompliant Strike User Name ---------------------------------------------------------------
        [HttpPut]
        [Route("AddNoncompliantStrikeUserName")]
        public async Task<ActionResult> AddNoncompliantStrikeUserName(AddNoncompliantStrikeUserNameCommand addNoncompliantStrikeUserName)
        {
            return SetResponse(await _mediator.Send(addNoncompliantStrikeUserName));
        }







        // -------------------------------------------------------------------------------- Archive ------------------------------------------------------------------------------
        [HttpPut]
        [Route("Archive")]
        public async Task<ActionResult> Archive(ArchiveCommand archive)
        {
            return SetResponse(await _mediator.Send(archive));
        }





        // --------------------------------------------------------------------------- Delete Notifications ----------------------------------------------------------------------
        [HttpDelete]
        [Route("DeleteNotifications")]
        public async Task<ActionResult> DeleteNotifications(Guid notificationGroupId, [FromQuery] List<Guid> notificationIds)
        {
            return SetResponse(await _mediator.Send(new DeleteNotificationsCommand(notificationGroupId, notificationIds)));
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







        // ------------------------------------------------------------------------- Get List Notification -----------------------------------------------------------------------
        [HttpGet]
        [Route("GetListNotification")]
        public async Task<ActionResult> GetListNotification(Guid notificationGroupId, bool isNew)
        {
            return SetResponse(await _mediator.Send(new GetListNotificationQuery(notificationGroupId, isNew)));
        }







        // ------------------------------------------------------------------------ Get Message Notification ---------------------------------------------------------------------
        [HttpGet]
        [Route("GetMessageNotification")]
        public async Task<ActionResult> GetMessageNotification(Guid notificationGroupId, bool isNew)
        {
            return SetResponse(await _mediator.Send(new GetMessageNotificationQuery(notificationGroupId, isNew)));
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







        // ------------------------------------------------------------------------ Get Product Notification ---------------------------------------------------------------------
        [HttpGet]
        [Route("GetProductNotification")]
        public async Task<ActionResult> GetProductNotification(Guid notificationGroupId)
        {
            return SetResponse(await _mediator.Send(new GetProductNotificationQuery(notificationGroupId)));
        }







        // ------------------------------------------------------------------- Get Review Complaint Notification -----------------------------------------------------------------
        [HttpGet]
        [Route("GetReviewComplaintNotification")]
        public async Task<ActionResult> GetReviewComplaintNotification(Guid notificationGroupId)
        {
            return SetResponse(await _mediator.Send(new GetReviewComplaintNotificationQuery(notificationGroupId)));
        }






        // ------------------------------------------------------------------------ Get Review Notification ----------------------------------------------------------------------
        [HttpGet]
        [Route("GetReviewNotification")]
        public async Task<ActionResult> GetReviewNotification(Guid notificationGroupId, bool isNew)
        {
            return SetResponse(await _mediator.Send(new GetReviewNotificationQuery(notificationGroupId, isNew)));
        }






        // ----------------------------------------------------------------------- Get User Image Notification -------------------------------------------------------------------
        [HttpGet]
        [Route("GetUserImageNotification")]
        public async Task<ActionResult> GetUserImageNotification(Guid notificationGroupId, bool isNew)
        {
            return SetResponse(await _mediator.Send(new GetUserImageNotificationQuery(notificationGroupId, isNew)));
        }










        // ----------------------------------------------------------------------- Get User Name Notification --------------------------------------------------------------------
        [HttpGet]
        [Route("GetUserNameNotification")]
        public async Task<ActionResult> GetUserNameNotification(Guid notificationGroupId, bool isNew)
        {
            return SetResponse(await _mediator.Send(new GetUserNameNotificationQuery(notificationGroupId, isNew)));
        }



        






        // ------------------------------------------------------------------------------- Post Note -----------------------------------------------------------------------------
        [HttpPost]
        [Route("PostNote")]
        public async Task<ActionResult> PostNote(PostNoteCommand postNote)
        {
            return SetResponse(await _mediator.Send(postNote));
        }

        




        




        // ----------------------------------------------------------------------------- Remove Review ---------------------------------------------------------------------------
        [HttpPut]
        [Route("RemoveReview")]
        public async Task<ActionResult> RemoveReview(RemoveReviewCommand RemoveReview)
        {
            return SetResponse(await _mediator.Send(RemoveReview));
        }
    }
}