using Manager.Application.Filters.AddFilter.Commands;
using Manager.Application.Filters.AddFilterOption.Commands;
using Manager.Application.Filters.CheckDuplicateFilterOption.Queries;
using Manager.Application.Filters.DeleteFilter.Commands;
using Manager.Application.Filters.DeleteFilterOption.Commands;
using Manager.Application.Filters.GetFilterOptionParent.Queries;
using Manager.Application.Filters.GetFilterOptions.Queries;
using Manager.Application.Filters.GetFilters.Queries;
using Manager.Application.Filters.SearchFilters.Queries;
using Manager.Application.Filters.SetFilterName.Commands;
using Manager.Application.Filters.SetFilterOptionName.Commands;
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






        // ------------------------------------------------------------- Check Duplicate Filter Option -------------------------------------------------------------------
        [HttpGet]
        [Route("Options/CheckDuplicate")]
        public async Task<ActionResult> CheckDuplicateFilterOption(Guid childId, string childName)
        {
            return SetResponse(await _mediator.Send(new CheckDuplicateFilterOptionQuery(childId, childName)));
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





        // ---------------------------------------------------------------- Get Filter Option Parent ---------------------------------------------------------------------
        [HttpGet]
        [Route("Options/Parent")]
        public async Task<ActionResult> GetFilterOptionParent(Guid childId)
        {
            return SetResponse(await _mediator.Send(new GetFilterOptionParentQuery(childId)));
        }









        // -------------------------------------------------------------------- Get Filter Options -----------------------------------------------------------------------
        [HttpGet]
        [Route("Options")]
        public async Task<ActionResult> GetFilterOptions(Guid parentId, Guid productId)
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
        public async Task<ActionResult> SearchFilters(Guid? productId, string searchTerm)
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