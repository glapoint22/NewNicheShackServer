using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;
using Website.Application.ImagesMedia.SaveImages.Commands;

namespace Website.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public class ImagesController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public ImagesController(ISender mediator)
        {
            _mediator = mediator;
        }


        // ------------------------------------------------------------------------------- Save Images ---------------------------------------------------------------------------
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> SaveImages(IFormCollection images)
        {
            return SetResponse(await _mediator.Send(new SaveImagesCommand(images)));
        }
    }
}