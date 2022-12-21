using Manager.Application.Vendors.AddVendor.Commands;
using Manager.Application.Vendors.DeleteVendor.Commands;
using Manager.Application.Vendors.GetVendor.Queries;
using Manager.Application.Vendors.GetVendorProducts.Queries;
using Manager.Application.Vendors.SearchVendors.Queries;
using Manager.Application.Vendors.UpdateVendor.Commands;
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




        // --------------------------------------------------------------------------------- Add Vendor --------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> AddVendor(AddVendorCommand addVendor)
        {
            return SetResponse(await _mediator.Send(addVendor));
        }




        // -------------------------------------------------------------------------------- Delete Vendor ------------------------------------------------------------------------
        [HttpDelete]
        public async Task<ActionResult> DeleteVendor(Guid id)
        {
            return SetResponse(await _mediator.Send(new DeleteVendorCommand(id)));
        }




        // --------------------------------------------------------------------------------- Get Vendor --------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetVendor(Guid id)
        {
            return SetResponse(await _mediator.Send(new GetVendorQuery(id)));
        }







        // ----------------------------------------------------------------------------- Get Vendor Products ---------------------------------------------------------------------
        [HttpGet]
        [Route("Products")]
        public async Task<ActionResult> GetVendorProducts(Guid id)
        {
            return SetResponse(await _mediator.Send(new GetVendorProductsQuery(id)));
        }







        // ------------------------------------------------------------------------------- Search Vendors ------------------------------------------------------------------------
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult> SearchVendors(string searchTerm)
        {
            return SetResponse(await _mediator.Send(new SearchVendorsQuery(searchTerm)));
        }





        // -------------------------------------------------------------------------------- Update Vendor ------------------------------------------------------------------------
        [HttpPut]
        public async Task<ActionResult> UpdateVendor(UpdateVendorCommand updateVendor)
        {
            return SetResponse(await _mediator.Send(updateVendor));
        }
    }
}
