﻿using Manager.Application.Keywords.AddAvailableKeyword.Commands;
using Manager.Application.Keywords.AddAvailableKeywordGroup.Commands;
using Manager.Application.Keywords.AddSelectedKeywordFromKeywordGroup.Commands;
using Manager.Application.Keywords.AddSelectedKeywordGroup.Commands;
using Manager.Application.Keywords.DeleteAvailableKeyword.Commands;
using Manager.Application.Keywords.DeleteAvailableKeywordGroup.Commands;
using Manager.Application.Keywords.DeleteSelectedKeyword.Commands;
using Manager.Application.Keywords.GetAvailableKeywordGroups.Queries;
using Manager.Application.Keywords.GetAvailableKeywords.Queries;
using Manager.Application.Keywords.GetSelectedKeywordGroups.Queries;
using Manager.Application.Keywords.GetSelectedKeywords.Queries;
using Manager.Application.Keywords.NewSelectedKeywordCommand.Commands;
using Manager.Application.Keywords.NewSelectedKeywordGroup.Commands;
using Manager.Application.Keywords.RemoveSelectedKeywordGroup.Commands;
using Manager.Application.Keywords.SearchKeywordGroups.Queries;
using Manager.Application.Keywords.UpdateAvailableKeywordGroupName.Commands;
using Manager.Application.Keywords.UpdateAvailableKeywordName.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public sealed class KeywordsController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public KeywordsController(ISender mediator)
        {
            _mediator = mediator;
        }



        // ------------------------------------------------------------------------- Add Available Keyword -----------------------------------------------------------------------
        [HttpPost]
        [Route("AvailableKeywords")]
        public async Task<ActionResult> AddAvailableKeyword(AddAvailableKeywordCommand addAvailableKeyword)
        {
            return SetResponse(await _mediator.Send(addAvailableKeyword));
        }








        // ---------------------------------------------------------------------- Add Available Keyword Group --------------------------------------------------------------------
        [HttpPost]
        [Route("AvailableKeywords/Groups")]
        public async Task<ActionResult> AddAvailableKeywordGroup(AddAvailableKeywordGroupCommand addAvailableKeywordGroup)
        {
            return SetResponse(await _mediator.Send(addAvailableKeywordGroup));
        }







        // --------------------------------------------------------------- Add Selected Keyword From Keyword Group ---------------------------------------------------------------
        [HttpPost]
        [Route("SelectedKeywords/Groups/AddKeyword")]
        public async Task<ActionResult> AddSelectedKeywordFromKeywordGroup(AddSelectedKeywordFromKeywordGroupCommand addSelectedKeywordFromKeywordGroup)
        {
            return SetResponse(await _mediator.Send(addSelectedKeywordFromKeywordGroup));
        }











        // ---------------------------------------------------------------------- Add Selected Keyword Group ---------------------------------------------------------------------
        [HttpPost]
        [Route("SelectedKeywords/Groups")]
        public async Task<ActionResult> AddSelectedKeywordGroup(AddSelectedKeywordGroupCommand addSelectedKeywordGroup)
        {
            return SetResponse(await _mediator.Send(addSelectedKeywordGroup));
        }










        // ------------------------------------------------------------------------ Delete Available Keyword ---------------------------------------------------------------------
        [HttpDelete]
        [Route("AvailableKeywords")]
        public async Task<ActionResult> DeleteAvailableKeyword(Guid id, Guid keywordGroupId)
        {
            return SetResponse(await _mediator.Send(new DeleteAvailableKeywordCommand(id, keywordGroupId)));
        }










        // --------------------------------------------------------------------- Delete Available Keyword Group ------------------------------------------------------------------
        [HttpDelete]
        [Route("AvailableKeywords/Groups")]
        public async Task<ActionResult> DeleteAvailableKeywordGroup(Guid id)
        {
            return SetResponse(await _mediator.Send(new DeleteAvailableKeywordGroupCommand(id)));
        }







        // ------------------------------------------------------------------------ Delete Selected Keyword ----------------------------------------------------------------------
        [HttpDelete]
        [Route("SelectedKeywords")]
        public async Task<ActionResult> DeleteSelectedKeyword(Guid id)
        {
            return SetResponse(await _mediator.Send(new DeleteSelectedKeywordCommand(id)));
        }









        // ---------------------------------------------------------------------- Get Available Keyword Groups -------------------------------------------------------------------
        [HttpGet]
        [Route("AvailableKeywords/Groups")]
        public async Task<ActionResult> GetAvailableKeywordGroups(string? productId)
        {
            return SetResponse(await _mediator.Send(new GetAvailableKeywordGroupsQuery(productId)));
        }










        // ------------------------------------------------------------------------- Get Available Keywords ----------------------------------------------------------------------
        [HttpGet]
        [Route("AvailableKeywords")]
        public async Task<ActionResult> GetAvailableKeywords(Guid parentId)
        {
            return SetResponse(await _mediator.Send(new GetAvailableKeywordsQuery(parentId)));
        }










        // ---------------------------------------------------------------------- Get Selected Keyword Groups --------------------------------------------------------------------
        [HttpGet]
        [Route("SelectedKeywords/Groups")]
        public async Task<ActionResult> GetSelectedKeywordGroups(string productId)
        {
            return SetResponse(await _mediator.Send(new GetSelectedKeywordGroupsQuery(productId)));
        }







        // ------------------------------------------------------------------------- Get Selected Keywords -----------------------------------------------------------------------
        [HttpGet]
        [Route("SelectedKeywords")]
        public async Task<ActionResult> GetSelectedKeywords(string productId, Guid parentId)
        {
            return SetResponse(await _mediator.Send(new GetSelectedKeywordsQuery(productId, parentId)));
        }







        // ------------------------------------------------------------------------- New Selected Keyword ------------------------------------------------------------------------
        [HttpPost]
        [Route("SelectedKeywords")]
        public async Task<ActionResult> NewSelectedKeyword(NewSelectedKeywordCommand newSelectedKeyword)
        {
            return SetResponse(await _mediator.Send(newSelectedKeyword));
        }










        // ---------------------------------------------------------------------- New Selected Keyword Group ---------------------------------------------------------------------
        [HttpPost]
        [Route("SelectedKeywords/Groups/New")]
        public async Task<ActionResult> NewSelectedKeywordGroup(NewSelectedKeywordGroupCommand newSelectedKeywordGroup)
        {
            return SetResponse(await _mediator.Send(newSelectedKeywordGroup));
        }







        // --------------------------------------------------------------------- Remove Selected Keyword Group -------------------------------------------------------------------
        [HttpDelete]
        [Route("SelectedKeywords/Groups")]
        public async Task<ActionResult> RemoveSelectedKeywordGroup(string productId, Guid keywordGroupId)
        {
            return SetResponse(await _mediator.Send(new RemoveSelectedKeywordGroupCommand(productId, keywordGroupId)));
        }









        // ------------------------------------------------------------------------- Search Keyword Groups -----------------------------------------------------------------------
        [HttpGet]
        [Route("KeywordGroups/Search")]
        public async Task<ActionResult> SearchKeywordGroups(string searchTerm)
        {
            return SetResponse(await _mediator.Send(new SearchKeywordGroupsQuery(searchTerm)));
        }






        // ------------------------------------------------------------------- Update Available Keyword Group Name ---------------------------------------------------------------
        [HttpPut]
        [Route("AvailableKeywords/Groups")]
        public async Task<ActionResult> UpdateAvailableKeywordGroupName(UpdateAvailableKeywordGroupNameCommand updateAvailableKeywordGroupName)
        {
            return SetResponse(await _mediator.Send(updateAvailableKeywordGroupName));
        }








        // ---------------------------------------------------------------------- Update Available Keyword Name ------------------------------------------------------------------
        [HttpPut]
        [Route("AvailableKeywords")]
        public async Task<ActionResult> UpdateAvailableKeywordName(UpdateAvailableKeywordNameCommand updateAvailableKeywordName)
        {
            return SetResponse(await _mediator.Send(updateAvailableKeywordName));
        }
    }
}
