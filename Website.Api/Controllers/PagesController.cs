using MediatR;
using Microsoft.AspNetCore.Mvc;
using Website.Application.Pages.GetBrowsePage.Queries;
using Website.Application.Pages.GetGridData.Queries;
using Website.Application.Pages.GetPage.Queries;
using Website.Application.Pages.GetSearchPage.Queries;

namespace Website.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class PagesController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public PagesController(ISender mediator)
        {
            _mediator = mediator;
        }



        // ------------------------------------------------------------------------- Get Page --------------------------------------------------------------------------
        [HttpGet("PageType")]
        public async Task<ActionResult> GetPage(int pageType)
        {
            return SetResponse(await _mediator.Send(new GetPageQuery(PageType: pageType)));
        }




        // ------------------------------------------------------------------------- Get Page --------------------------------------------------------------------------
        [HttpGet("PageId")]
        public async Task<ActionResult> GetPage(string id)
        {
            return SetResponse(await _mediator.Send(new GetPageQuery(Id: id)));
        }






        // ---------------------------------------------------------------------- Get Search Page ----------------------------------------------------------------------
        [HttpGet]
        [Route("GetSearchPage")]
        public async Task<ActionResult> GetSearchPage(string searchTerm, string? nicheId, string? subnicheId, string? filters, string? sortBy, int page = 1)
        {

            return SetResponse(await _mediator.Send(new GetSearchPageQuery(searchTerm, nicheId, subnicheId, filters, page, sortBy)));
        }





        // ---------------------------------------------------------------------- Get Browse Page ----------------------------------------------------------------------
        [HttpGet]
        [Route("GetBrowsePage")]
        public async Task<ActionResult> GetBrowsePage(string? nicheId, string? subnicheId, string? filters, string? sortBy, int page = 1)
        {

            return SetResponse(await _mediator.Send(new GetBrowsePageQuery(nicheId, subnicheId, filters, page, sortBy)));
        }





        // ----------------------------------------------------------------------- Get Grid Data -----------------------------------------------------------------------
        [HttpGet]
        [Route("GetGridData")]
        public async Task<ActionResult> GetGridData(string? searchTerm, string? nicheId, string? subnicheId, string? filters, string? sortBy, int page = 1)
        {

            return SetResponse(await _mediator.Send(new GetGridDataQuery(searchTerm, nicheId, subnicheId, filters, page, sortBy)));
        }
    }
}