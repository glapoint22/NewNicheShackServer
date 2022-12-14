using Manager.Application.Subniches.AddSubniche.Commands;
using Manager.Application.Subniches.CheckDuplicateSubniche.Queries;
using Manager.Application.Subniches.DeleteSubniche.Commands;
using Manager.Application.Subniches.GetSubnicheParent.Queries;
using Manager.Application.Subniches.GetSubniches.Queries;
using Manager.Application.Subniches.SearchSubniches.Queries;
using Manager.Application.Subniches.UpdateSubnicheName.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public sealed class SubnichesController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public SubnichesController(ISender mediator)
        {
            _mediator = mediator;
        }


        // -------------------------------------------------------------------------------- Add Subniche -------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> AddSubniche(AddSubnicheCommand addSubniche)
        {
            return SetResponse(await _mediator.Send(addSubniche));
        }





        // --------------------------------------------------------------------------- Check Duplicate Subniche ------------------------------------------------------------------
        [HttpGet]
        [Route("CheckDuplicate")]
        public async Task<ActionResult> CheckDuplicateSubniche(Guid childId, string childName)
        {
            return SetResponse(await _mediator.Send(new CheckDuplicateSubnicheQuery(childId, childName)));
        }







        // ------------------------------------------------------------------------------ Delete Subniche ------------------------------------------------------------------------
        [HttpDelete]
        public async Task<ActionResult> DeleteSubniche(Guid id)
        {
            return SetResponse(await _mediator.Send(new DeleteSubnicheCommand(id)));
        }





        // ----------------------------------------------------------------------------- Get Subniche Parent ---------------------------------------------------------------------
        [HttpGet]
        [Route("Parent")]
        public async Task<ActionResult> GetSubnicheParent(Guid childId)
        {
            return SetResponse(await _mediator.Send(new GetSubnicheParentQuery(childId)));
        }







        // -------------------------------------------------------------------------------- Get Subniches ------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetSubniches(Guid parentId)
        {
            return SetResponse(await _mediator.Send(new GetSubnichesQuery(parentId)));
        }


        // ------------------------------------------------------------------------------ Search Subniches -----------------------------------------------------------------------
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult> SearchSubniches(string searchTerm)
        {
            return SetResponse(await _mediator.Send(new SearchSubnichesQuery(searchTerm)));
        }




        // ---------------------------------------------------------------------------- Update Subniche Name ---------------------------------------------------------------------
        [HttpPut]
        public async Task<ActionResult> UpdateSubnicheName(UpdateSubnicheNameCommand updateSubnicheName)
        {
            return SetResponse(await _mediator.Send(updateSubnicheName));
        }
    }
}