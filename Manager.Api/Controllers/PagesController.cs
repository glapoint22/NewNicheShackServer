using Manager.Application.Pages.AddPageKeywordGroup.Commands;
using Manager.Application.Pages.AddPageSubniche.Commands;
using Manager.Application.Pages.DeletePage.Commands;
using Manager.Application.Pages.DeletePageKeywordGroup.Commands;
using Manager.Application.Pages.DeletePageSubniche.Commands;
using Manager.Application.Pages.DuplicatePage.Commands;
using Manager.Application.Pages.GetPage.Queries;
using Manager.Application.Pages.GetPageKeywordGroups.Queries;
using Manager.Application.Pages.GetPageKeywords.Queries;
using Manager.Application.Pages.GetPageSubniches.Queries;
using Manager.Application.Pages.NewPage.Commands;
using Manager.Application.Pages.SearchLinkPages.Queries;
using Manager.Application.Pages.SearchPages.Queries;
using Manager.Application.Pages.UpdatePage.Commands;
using Manager.Application.Pages.UpdatePageKeyword.Commands;
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



        // ------------------------------------------------------------------------- Add Page Keyword Group ----------------------------------------------------------------------
        [HttpPost]
        [Route("KeywordGroup")]
        public async Task<ActionResult> AddPageKeywordGroup(AddPageKeywordGroupCommand addPageKeywordGroup)
        {
            return SetResponse(await _mediator.Send(addPageKeywordGroup));
        }





        // --------------------------------------------------------------------------- Add Page Subniche -------------------------------------------------------------------------
        [HttpPost]
        [Route("Subniche")]
        public async Task<ActionResult> AddPageSubniche(AddPageSubnicheCommand addPageSubniche)
        {
            return SetResponse(await _mediator.Send(addPageSubniche));
        }




        // ------------------------------------------------------------------------------- Delete Page ---------------------------------------------------------------------------
        [HttpDelete]
        public async Task<ActionResult> DeletePage(string pageId)
        {
            return SetResponse(await _mediator.Send(new DeletePageCommand(pageId)));
        }





        // ------------------------------------------------------------------------ Delete Page Keyword Group --------------------------------------------------------------------
        [HttpDelete]
        [Route("KeywordGroup")]
        public async Task<ActionResult> DeletePageKeywordGroup(string pageId, Guid id)
        {
            return SetResponse(await _mediator.Send(new DeletePageKeywordGroupCommand(pageId, id)));
        }






        // -------------------------------------------------------------------------- Delete Page Subniche -----------------------------------------------------------------------
        [HttpDelete]
        [Route("Subniche")]
        public async Task<ActionResult> DeletePageSubniche(string pageId, string id)
        {
            return SetResponse(await _mediator.Send(new DeletePageSubnicheCommand(pageId, id)));
        }





        // ----------------------------------------------------------------------------- Duplicate Page --------------------------------------------------------------------------
        [HttpPost]
        [Route("Duplicate")]
        public async Task<ActionResult> DuplicatePage(DuplicatePageCommand duplicatePage)
        {
            return SetResponse(await _mediator.Send(duplicatePage));
        }







        // -------------------------------------------------------------------------------- Get Page -----------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetPage(string id)
        {
            return SetResponse(await _mediator.Send(new GetPageQuery(id)));
        }




        // ------------------------------------------------------------------------- Get Page Keyword Groups ---------------------------------------------------------------------
        [HttpGet]
        [Route("KeywordGroup")]
        public async Task<ActionResult> GetPageKeywordGroups(string pageId)
        {
            return SetResponse(await _mediator.Send(new GetPageKeywordGroupsQuery(pageId)));
        }






        // ---------------------------------------------------------------------------- Get Page Keywords ------------------------------------------------------------------------
        [HttpGet]
        [Route("Keywords")]
        public async Task<ActionResult> GetPageKeywords(string pageId, Guid keywordGroupId)
        {
            return SetResponse(await _mediator.Send(new GetPageKeywordsQuery(pageId, keywordGroupId)));
        }







        // ---------------------------------------------------------------------------- Get Page Subniches -----------------------------------------------------------------------
        [HttpGet]
        [Route("Subniche")]
        public async Task<ActionResult> GetPageSubniches(string pageId)
        {
            return SetResponse(await _mediator.Send(new GetPageSubnichesQuery(pageId)));
        }




        // -------------------------------------------------------------------------------- New Page -----------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> NewPage(NewPageCommand newPage)
        {
            return SetResponse(await _mediator.Send(newPage));
        }




        // ----------------------------------------------------------------------------- Search Link Pages -----------------------------------------------------------------------
        [HttpGet]
        [Route("LinkSearch")]
        public async Task<ActionResult> SearchLinkPages(string searchTerm)
        {
            return SetResponse(await _mediator.Send(new SearchLinkPagesQuery(searchTerm)));
        }






        // ------------------------------------------------------------------------------ Search Pages ---------------------------------------------------------------------------
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult> SearchPages(string searchTerm)
        {
            return SetResponse(await _mediator.Send(new SearchPagesQuery(searchTerm)));
        }





        // ------------------------------------------------------------------------------- Update Page ---------------------------------------------------------------------------
        [HttpPut]
        public async Task<ActionResult> UpdatePage(UpdatePageCommand updatePage)
        {
            return SetResponse(await _mediator.Send(updatePage));
        }




        // -------------------------------------------------------------------------- Update Page Keyword ------------------------------------------------------------------------
        [HttpPut]
        [Route("Keywords")]
        public async Task<ActionResult> UpdatePageKeyword(UpdatePageKeywordCommand updatePageKeyword)
        {
            return SetResponse(await _mediator.Send(updatePageKeyword));
        }
    }
}
