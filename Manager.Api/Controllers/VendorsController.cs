using Manager.Application.Vendors.SearchVendors.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public sealed class VendorsController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public VendorsController(ISender mediator)
        {
            _mediator = mediator;
        }




        // ----------------------------------------------------------------------------- Search Vendors --------------------------------------------------------------------------
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult> SearchVendors(string searchTerm)
        {
            return SetResponse(await _mediator.Send(new SearchVendorsQuery(searchTerm)));
        }
    }
}
