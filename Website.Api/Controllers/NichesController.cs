using MediatR;
using Microsoft.AspNetCore.Mvc;
using Website.Application.Niches.GetNiches.Queries;
using Website.Application.Niches.GetSubniches.Queries;

namespace Website.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public sealed class NichesController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public NichesController(ISender mediator)
        {
            _mediator = mediator;
        }



        // ----------------------------------------------------------------------- Get Niches ----------------------------------------------------------------------------
        [HttpGet]
        [Route("GetNiches")]
        public async Task<ActionResult> GetNiches()
        {
            return SetResponse(await _mediator.Send(new GetNichesQuery()));
        }





        // ---------------------------------------------------------------------- Get Subniches ----------------------------------------------------------------------------
        [HttpGet]
        [Route("GetSubniches")]
        public async Task<ActionResult> GetSubniches(int nicheId)
        {
            return SetResponse(await _mediator.Send(new GetSubnichesQuery(nicheId)));
        }
    }
}