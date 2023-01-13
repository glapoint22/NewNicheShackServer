using Manager.Application._Publish.GetPublishItems.Queries;
using Manager.Application._Publish.PublishProduct.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public sealed class PublishController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public PublishController(ISender mediator)
        {
            _mediator = mediator;
        }



        // ---------------------------------------------------------------------------- Get Publish Items ------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetPublishItems()
        {
            return SetResponse(await _mediator.Send(new GetPublishItemsQuery()));
        }






        // ------------------------------------------------------------------------------ Publish Product ------------------------------------------------------------------------
        [HttpPost]
        [Route("PublishProduct")]
        public async Task<ActionResult> PublishProduct(PublishProductCommand publishProduct)
        {
            return SetResponse(await _mediator.Send(publishProduct));
        }
    }
}
