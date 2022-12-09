using Manager.Application.Products.AddPricePoint.Commands;
using Manager.Application.Products.AddSubproduct.Commands;
using Manager.Application.Products.GetProduct.Queries;
using Manager.Application.Products.GetProducts.Queries;
using Manager.Application.Products.RemovePricePoint.Commands;
using Manager.Application.Products.RemoveProductMedia.Commands;
using Manager.Application.Products.RemoveSubproduct.Commands;
using Manager.Application.Products.SearchProducts.Queries;
using Manager.Application.Products.SetDescription.Commands;
using Manager.Application.Products.SetHoplink.Commands;
using Manager.Application.Products.SetName.Commands;
using Manager.Application.Products.SetPrice.Commands;
using Manager.Application.Products.SetPricePoint.Commands;
using Manager.Application.Products.SetProductMedia.Commands;
using Manager.Application.Products.SetProductMediaIndices.Commands;
using Manager.Application.Products.SetRecurringPayment.Commands;
using Manager.Application.Products.SetShipping.Commands;
using Manager.Application.Products.SetSubproduct.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public class ProductsController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public ProductsController(ISender mediator)
        {
            _mediator = mediator;
        }




        // ------------------------------------------------------------------------------- Add Price Point -----------------------------------------------------------------------
        [HttpPost]
        [Route("PricePoint")]
        public async Task<ActionResult> AddPricePoint(AddPricePointCommand addPricePoint)
        {
            return SetResponse(await _mediator.Send(addPricePoint));
        }






        // ------------------------------------------------------------------------------- Add Subproduct ------------------------------------------------------------------------
        [HttpPost]
        [Route("Subproduct")]
        public async Task<ActionResult> AddSubproduct(AddSubproductCommand addSubproduct)
        {
            return SetResponse(await _mediator.Send(addSubproduct));
        }









        // --------------------------------------------------------------------------------- Get Product -------------------------------------------------------------------------
        [HttpGet]
        [Route("Product")]
        public async Task<ActionResult> GetProduct(string productId)
        {
            return SetResponse(await _mediator.Send(new GetProductQuery(productId)));
        }







        // -------------------------------------------------------------------------------- Get Products -------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetProducts(string parentId)
        {
            return SetResponse(await _mediator.Send(new GetProductsQuery(parentId)));
        }






        // ------------------------------------------------------------------------------ Remove Price Point ---------------------------------------------------------------------
        [HttpDelete]
        [Route("PricePoint")]
        public async Task<ActionResult> RemovePricePoint(string productId, Guid pricePointId)
        {
            return SetResponse(await _mediator.Send(new RemovePricePointCommand(productId, pricePointId)));
        }






        // ----------------------------------------------------------------------------- Remove Product Media --------------------------------------------------------------------
        [HttpDelete]
        [Route("Media")]
        public async Task<ActionResult> RemoveProductMedia(string productId, Guid productMediaId)
        {
            return SetResponse(await _mediator.Send(new RemoveProductMediaCommand(productId, productMediaId)));
        }






        // ------------------------------------------------------------------------------ Remove Subproduct ----------------------------------------------------------------------
        [HttpDelete]
        [Route("Subproduct")]
        public async Task<ActionResult> RemoveSubproduct(string productId, Guid subproductId)
        {
            return SetResponse(await _mediator.Send(new RemoveSubproductCommand(productId, subproductId)));
        }







        // ------------------------------------------------------------------------------ Search Products ------------------------------------------------------------------------
        [HttpGet]
        [Route("LinkSearch")]
        public async Task<ActionResult> SearchProducts(string searchTerm)
        {
            return SetResponse(await _mediator.Send(new SearchProductsQuery(searchTerm)));
        }





        // ------------------------------------------------------------------------------ Set Description ------------------------------------------------------------------------
        [HttpPut]
        [Route("Description")]
        public async Task<ActionResult> SetDescription(SetDescriptionCommand setDescription)
        {
            return SetResponse(await _mediator.Send(setDescription));
        }







        // -------------------------------------------------------------------------------- Set Hoplink --------------------------------------------------------------------------
        [HttpPut]
        [Route("Hoplink")]
        public async Task<ActionResult> SetHoplink(SetHoplinkCommand setHoplink)
        {
            return SetResponse(await _mediator.Send(setHoplink));
        }











        // --------------------------------------------------------------------------------- Set Name ----------------------------------------------------------------------------
        [HttpPut]
        public async Task<ActionResult> SetName(SetNameCommand setName)
        {
            return SetResponse(await _mediator.Send(setName));
        }













        // ---------------------------------------------------------------------------------- Set Price --------------------------------------------------------------------------
        [HttpPost]
        [Route("Price")]
        public async Task<ActionResult> SetPrice(SetPriceCommand setPrice)
        {
            return SetResponse(await _mediator.Send(setPrice));
        }









        // ------------------------------------------------------------------------------- Set Price Point -----------------------------------------------------------------------
        [HttpPut]
        [Route("PricePoint")]
        public async Task<ActionResult> SetPricePoint(SetPricePointCommand setPricePoint)
        {
            return SetResponse(await _mediator.Send(setPricePoint));
        }














        // ----------------------------------------------------------------------------- Set Product Media -----------------------------------------------------------------------
        [HttpPut]
        [Route("Media")]
        public async Task<ActionResult> SetProductMedia(SetProductMediaCommand setProductMedia)
        {
            return SetResponse(await _mediator.Send(setProductMedia));
        }












        // ------------------------------------------------------------------------- Set Product Media Indices -------------------------------------------------------------------
        [HttpPut]
        [Route("MediaIndices")]
        public async Task<ActionResult> SetProductMediaIndices(SetProductMediaIndicesCommand updateMediaIndices)
        {
            return SetResponse(await _mediator.Send(updateMediaIndices));
        }













        // ---------------------------------------------------------------------------- Set Recurring Payment --------------------------------------------------------------------
        [HttpPut]
        [Route("RecurringPayment")]
        public async Task<ActionResult> SetRecurringPayment(SetRecurringPaymentCommand setRecurringPayment)
        {
            return SetResponse(await _mediator.Send(setRecurringPayment));
        }









        // -------------------------------------------------------------------------------- Set Shipping -------------------------------------------------------------------------
        [HttpPut]
        [Route("Shipping")]
        public async Task<ActionResult> SetShipping(SetShippingCommand setShipping)
        {
            return SetResponse(await _mediator.Send(setShipping));
        }








        // ------------------------------------------------------------------------------- Set Subproduct ------------------------------------------------------------------------
        [HttpPut]
        [Route("Subproduct")]
        public async Task<ActionResult> SetSubproduct(SetSubproductCommand setSubproduct)
        {
            return SetResponse(await _mediator.Send(setSubproduct));
        }
    }
}