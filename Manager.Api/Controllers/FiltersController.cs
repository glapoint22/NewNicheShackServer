﻿using Manager.Application.Filters.AddFilter.Commands;
using Manager.Application.Filters.AddFilterOption.Commands;
using Manager.Application.Filters.DeleteFilter.Commands;
using Manager.Application.Filters.DeleteFilterOption.Commands;
using Manager.Application.Filters.GetFilterOptions.Queries;
using Manager.Application.Filters.GetFilters.Queries;
using Manager.Application.Filters.SearchFilters.Queries;
using Manager.Application.Filters.SetFilterName.Commands;
using Manager.Application.Filters.SetFilterOptionName.Commands;
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
    public sealed class FiltersController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public FiltersController(ISender mediator)
        {
            _mediator = mediator;
        }



        // ----------------------------------------------------------------------- Add Filter ----------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> AddFilter(AddFilterCommand addFilter)
        {
            return SetResponse(await _mediator.Send(addFilter));
        }






        // ------------------------------------------------------------------- Add Filter Option -------------------------------------------------------------------------
        [HttpPost]
        [Route("Options")]
        public async Task<ActionResult> AddFilterOption(AddFilterOptionCommand addFilterOption)
        {
            return SetResponse(await _mediator.Send(addFilterOption));
        }







        // --------------------------------------------------------------------- Delete Filter ---------------------------------------------------------------------------
        [HttpDelete]
        public async Task<ActionResult> DeleteFilter(Guid id)
        {
            return SetResponse(await _mediator.Send(new DeleteFilterCommand(id)));
        }





        // ------------------------------------------------------------------ Delete Filter Option -----------------------------------------------------------------------
        [HttpDelete]
        [Route("Options")]
        public async Task<ActionResult> DeleteFilterOption(Guid id)
        {
            return SetResponse(await _mediator.Send(new DeleteFilterOptionCommand(id)));
        }







        // -------------------------------------------------------------------- Get Filter Options -----------------------------------------------------------------------
        [HttpGet]
        [Route("Options")]
        public async Task<ActionResult> GetFilterOptions(Guid parentId, string productId)
        {
            return SetResponse(await _mediator.Send(new GetFilterOptionsQuery(parentId, productId)));
        }









        // ----------------------------------------------------------------------- Get Filters ---------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetFilters()
        {
            return SetResponse(await _mediator.Send(new GetFiltersQuery()));
        }








        // --------------------------------------------------------------------- Search Filters --------------------------------------------------------------------------
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult> SearchFilters(string? productId, string searchTerm)
        {
            return SetResponse(await _mediator.Send(new SearchFiltersQuery(productId, searchTerm)));
        }











        // --------------------------------------------------------------------- Set Filter Name -------------------------------------------------------------------------
        [HttpPut]
        public async Task<ActionResult> SetFilterName(SetFilterNameCommand setFilterName)
        {
            return SetResponse(await _mediator.Send(setFilterName));
        }









        // ------------------------------------------------------------------ Set Filter Option Name ---------------------------------------------------------------------
        [HttpPut]
        [Route("Options")]
        public async Task<ActionResult> SetFilterOptionName(SetFilterOptionNameCommand setFilterOptionName)
        {
            return SetResponse(await _mediator.Send(setFilterOptionName));
        }
    }
}