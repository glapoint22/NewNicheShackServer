using Manager.Application.Niches.GetNiches.Queries;
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

        // ----------------------------------------------------------------------- Get Niches ----------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetNiches()
        {
            return SetResponse(await _mediator.Send(new GetNichesQuery()));
        }
    }
}
