using Manager.Application.Niches.AddNiche.Commands;
using Manager.Application.Niches.DeleteNiche.Commands;
using Manager.Application.Niches.GetNicheChildren.Queries;
using Manager.Application.Niches.GetNiches.Queries;
using Manager.Application.Niches.SearchNiches.Queries;
using Manager.Application.Niches.UpdateNicheName.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public sealed class NichesController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public NichesController(ISender mediator)
        {
            _mediator = mediator;
        }



        // ---------------------------------------------------------------------------------- Add Niche --------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> AddNiche(AddNicheCommand addNiche)
        {
            return SetResponse(await _mediator.Send(addNiche));
        }


        // -------------------------------------------------------------------------------- Delete Niche -------------------------------------------------------------------------
        [HttpDelete]
        public async Task<ActionResult> DeleteNiche(Guid id)
        {
            return SetResponse(await _mediator.Send(new DeleteNicheCommand(id)));
        }




        // ------------------------------------------------------------------------------ Get Niche Children ---------------------------------------------------------------------
        [HttpGet]
        [Route("Children")]
        public async Task<ActionResult> GetNicheChildren(Guid parentId)
        {
            return SetResponse(await _mediator.Send(new GetNicheChildrenQuery(parentId)));
        }





        // --------------------------------------------------------------------------------- Get Niches --------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetNiches()
        {
            return SetResponse(await _mediator.Send(new GetNichesQuery()));
        }



        // -------------------------------------------------------------------------------- Search Niches ------------------------------------------------------------------------
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult> SearchNiches(string searchTerm)
        {
            return SetResponse(await _mediator.Send(new SearchNichesQuery(searchTerm)));
        }




        // ------------------------------------------------------------------------------ Update Niche Name ----------------------------------------------------------------------
        [HttpPut]
        public async Task<ActionResult> UpdateNicheName(UpdateNicheNameCommand updateNicheName)
        {
            return SetResponse(await _mediator.Send(updateNicheName));
        }
    }
}