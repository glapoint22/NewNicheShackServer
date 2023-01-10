using Manager.Application.Notifications.GetArchivedNotifications.Queries;
using Manager.Application.PublishItems.GetPublishItems.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    public sealed class PublishItemsController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public PublishItemsController(ISender mediator)
        {
            _mediator = mediator;
        }



        // ---------------------------------------------------------------------------- Get Publish Items ------------------------------------------------------------------------
        [HttpGet]
        [Route("GetPublishItems")]
        public async Task<ActionResult> GetPublishItems()
        {
            return SetResponse(await _mediator.Send(new GetPublishItemsQuery()));
        }
    }
}
