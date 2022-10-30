using MediatR;
using Microsoft.AspNetCore.Mvc;
using Website.Application.Pages.GetSearchPage.Queries;

namespace Website.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public sealed class PagesController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public PagesController(ISender mediator)
        {
            _mediator = mediator;
        }



        // ------------------------------------------------------------------------- Get Page --------------------------------------------------------------------------
        //[HttpGet]
        //[Route("GetPage")]
        //public async Task<ActionResult> GetPage(string? id = null, int? pageType = null)
        //{
        //    return SetResponse(await _mediator.Send(GetPageQuery(id, pageType)));
        //}






        // ---------------------------------------------------------------------- Get Search Page ----------------------------------------------------------------------
        [HttpGet]
        [Route("GetSearchPage")]
        public async Task<ActionResult> GetSearchPage(string searchTerm, int? nicheId, int? subnicheId, string? filters, string? sortBy, int page = 1)
        {

            return SetResponse(await _mediator.Send(new GetSearchPageQuery(searchTerm, nicheId, subnicheId, filters, page, sortBy)));
        }
    }
}
