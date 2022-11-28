using Manager.Application.Medias.AddImageSize.Commands;
using Manager.Application.Medias.DeleteMedia.Commands;
using Manager.Application.Medias.PostNewImage.Commands;
using Manager.Application.Medias.PostVideo.Commands;
using Manager.Application.Medias.SearchMedia.Queries;
using Manager.Application.Medias.UpdateImage.Commands;
using Manager.Application.Medias.UpdateVideo.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public class MediaController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public MediaController(ISender mediator)
        {
            _mediator = mediator;
        }



        // ------------------------------------------------------------------------------ Add Image Size -------------------------------------------------------------------------
        [HttpPost]
        [Route("AddImageSize")]
        public async Task<ActionResult> AddImageSize(AddImageSizeCommand addImageSize)
        {
            return SetResponse(await _mediator.Send(addImageSize));
        }




        // ------------------------------------------------------------------------------- Delete Media --------------------------------------------------------------------------
        [HttpDelete]
        public async Task<ActionResult> DeleteMedia(Guid id)
        {
            return SetResponse(await _mediator.Send(new DeleteMediaCommand(id)));
        }





        // -------------------------------------------------------------------------------- Post Image ---------------------------------------------------------------------------
        [HttpPost, DisableRequestSizeLimit]
        [Route("Image")]
        public async Task<ActionResult> PostImage()
        {
            return SetResponse(await _mediator.Send(new PostImageCommand(Request.Form)));
        }






        // -------------------------------------------------------------------------------- Post Video ---------------------------------------------------------------------------
        [HttpPost]
        [Route("Video")]
        public async Task<ActionResult> PostVideo(PostVideoCommand postVideo)
        {
            return SetResponse(await _mediator.Send(postVideo));
        }






        // ------------------------------------------------------------------------------- Search Media --------------------------------------------------------------------------
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult> SearchMedia(int mediaType, string searchTerm)
        {
            return SetResponse(await _mediator.Send(new SearchMediaQuery(mediaType, searchTerm)));
        }





        // ------------------------------------------------------------------------------- Update Image --------------------------------------------------------------------------
        [HttpPost, DisableRequestSizeLimit]
        [Route("UpdateImage")]
        public async Task<ActionResult> UpdateImage()
        {
            return SetResponse(await _mediator.Send(new UpdateImageCommand(Request.Form)));
        }




        


        // ------------------------------------------------------------------------------ Update Video ---------------------------------------------------------------------------
        [HttpPut]
        [Route("UpdateVideo")]
        public async Task<ActionResult> UpdateVideo(UpdateVideoCommand updateVideo)
        {
            return SetResponse(await _mediator.Send(updateVideo));
        }
    }
}