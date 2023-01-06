using Manager.Application.Notifications.ArchiveAllNotifications.Commands;
using Manager.Application.Notifications.ArchiveGroup.Commands;
using Manager.Application.Notifications.ArchiveNotification.Commands;
using Manager.Application.Notifications.DeleteNotifications.Commands;
using Manager.Application.Notifications.DisableEnableProduct.Commands;
using Manager.Application.Notifications.GetArchivedNotifications.Queries;
using Manager.Application.Notifications.GetBlockedUsers.Queries;
using Manager.Application.Notifications.GetErrorNotification.Queries;
using Manager.Application.Notifications.GetListNotification.Queries;
using Manager.Application.Notifications.GetMessageNotification.Queries;
using Manager.Application.Notifications.GetNewNotifications.Queries;
using Manager.Application.Notifications.GetNoncompliantUsers.Queries;
using Manager.Application.Notifications.GetNotificationCount.Queries;
using Manager.Application.Notifications.GetProductNotification.Queries;
using Manager.Application.Notifications.GetReviewComplaintNotification.Queries;
using Manager.Application.Notifications.GetReviewNotification.Queries;
using Manager.Application.Notifications.GetUserImageNotification.Queries;
using Manager.Application.Notifications.GetUserNameNotification.Queries;
using Manager.Application.Notifications.PostNote.Commands;
using Manager.Application.Notifications.ReformList.Commands;
using Manager.Application.Notifications.RemoveReview.Commands;
using Manager.Application.Notifications.RemoveUserImage.Commands;
using Manager.Application.Notifications.ReplaceUserName.Commands;
using Manager.Application.Notifications.RestoreAllNotifications.Commands;
using Manager.Application.Notifications.RestoreGroup.Commands;
using Manager.Application.Notifications.RestoreNotification.Commands;
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



        // ----------------------------------------------------------------------- Archive All Notifications ---------------------------------------------------------------------
        [HttpPut]
        [Route("ArchiveAllNotifications")]
        public async Task<ActionResult> ArchiveAllNotifications(ArchiveAllNotificationsCommand archiveAllNotifications)
        {
            return SetResponse(await _mediator.Send(archiveAllNotifications));
        }



        // ----------------------------------------------------------------------------- Archive Group ---------------------------------------------------------------------------
        [HttpPut]
        [Route("ArchiveGroup")]
        public async Task<ActionResult> ArchiveGroup(ArchiveGroupCommand archiveGroup)
        {
            return SetResponse(await _mediator.Send(archiveGroup));
        }




        // ------------------------------------------------------------------------- Archive Notification ------------------------------------------------------------------------
        [HttpPut]
        [Route("ArchiveNotification")]
        public async Task<ActionResult> ArchiveNotification(ArchiveNotificationCommand archiveNotification)
        {
            return SetResponse(await _mediator.Send(archiveNotification));
        }





        // --------------------------------------------------------------------------- Delete Notifications ----------------------------------------------------------------------
        [HttpDelete]
        [Route("DeleteNotifications")]
        public async Task<ActionResult> DeleteNotifications(Guid notificationGroupId, [FromQuery] List<Guid> notificationIds)
        {
            return SetResponse(await _mediator.Send(new DeleteNotificationsCommand(notificationGroupId, notificationIds)));
        }








        // -------------------------------------------------------------------------- Disable Enable Product ---------------------------------------------------------------------
        [HttpPut]
        [Route("DisableEnableProduct")]
        public async Task<ActionResult> DisableEnableProduct(DisableEnableProductCommand disableEnableProduct)
        {
            return SetResponse(await _mediator.Send(disableEnableProduct));
        }








        // ----------------------------------------------------------------------- Get Archived Notifications --------------------------------------------------------------------
        [HttpGet]
        [Route("GetArchivedNotifications")]
        public async Task<ActionResult> GetArchivedNotifications()
        {
            return SetResponse(await _mediator.Send(new GetArchivedNotificationsQuery()));
        }







        // ---------------------------------------------------------------------------- Get Blocked Users ------------------------------------------------------------------------
        [HttpGet]
        [Route("BlockedUsers")]
        public async Task<ActionResult> GetBlockedUsers()
        {
            return SetResponse(await _mediator.Send(new GetBlockedUsersQuery()));
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








        // ------------------------------------------------------------------------- Get Noncompliant Users ----------------------------------------------------------------------
        [HttpGet]
        [Route("NoncompliantUsers")]
        public async Task<ActionResult> GetNoncompliantUsers()
        {
            return SetResponse(await _mediator.Send(new GetNoncompliantUsersQuery()));
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





        // ------------------------------------------------------------------------------ Reform List ----------------------------------------------------------------------------
        [HttpPut]
        [Route("ReformList")]
        public async Task<ActionResult> ReformList(ReformListCommand reformList)
        {
            return SetResponse(await _mediator.Send(reformList));
        }





        // ------------------------------------------------------------------------------- Remove Review ---------------------------------------------------------------------------
        [HttpPut]
        [Route("RemoveReview")]
        public async Task<ActionResult> RemoveReview(RemoveReviewCommand removeReview)
        {
            return SetResponse(await _mediator.Send(removeReview));
        }





        // ----------------------------------------------------------------------------- Remove User Image -----------------------------------------------------------------------
        [HttpPut]
        [Route("RemoveUserImage")]
        public async Task<ActionResult> RemoveUserImage(RemoveUserImageCommand removeUserImage)
        {
            return SetResponse(await _mediator.Send(removeUserImage));
        }




        // ----------------------------------------------------------------------------- Replace User Name -----------------------------------------------------------------------
        [HttpPut]
        [Route("ReplaceUserName")]
        public async Task<ActionResult> ReplaceUserName(ReplaceUserNameCommand replaceUserName)
        {
            return SetResponse(await _mediator.Send(replaceUserName));
        }




        // ----------------------------------------------------------------------- Restore All Notifications ---------------------------------------------------------------------
        [HttpPut]
        [Route("RestoreAllNotifications")]
        public async Task<ActionResult> RestoreAllNotifications(RestoreAllNotificationsCommand restoreAllNotifications)
        {
            return SetResponse(await _mediator.Send(restoreAllNotifications));
        }




        // ----------------------------------------------------------------------------- Restore Group ---------------------------------------------------------------------------
        [HttpPut]
        [Route("RestoreGroup")]
        public async Task<ActionResult> RestoreGroup(RestoreGroupCommand restoreGroup)
        {
            return SetResponse(await _mediator.Send(restoreGroup));
        }





        // ------------------------------------------------------------------------- Restore Notification ------------------------------------------------------------------------
        [HttpPut]
        [Route("RestoreNotification")]
        public async Task<ActionResult> RestoreNotification(RestoreNotificationCommand restoreNotification)
        {
            return SetResponse(await _mediator.Send(restoreNotification));
        }
    }
}