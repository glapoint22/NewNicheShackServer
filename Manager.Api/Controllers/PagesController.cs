using Manager.Application.Pages.GetPage.Queries;
using Manager.Application.Pages.SearchPages.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public class PagesController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public PagesController(ISender mediator)
        {
            _mediator = mediator;
        }


        // -------------------------------------------------------------------------------- Get Page -----------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetPage(string pageId)
        {
            return SetResponse(await _mediator.Send(new GetPageQuery(pageId)));
        }





        // ------------------------------------------------------------------------------ Search Pages ---------------------------------------------------------------------------
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult> SearchPages(string searchTerm)
        {
            return SetResponse(await _mediator.Send(new SearchPagesQuery(searchTerm)));
        }
    }
}
