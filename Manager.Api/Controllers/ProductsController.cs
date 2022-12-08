using Manager.Application.Products.AddPricePoint.Commands;
using Manager.Application.Products.AddSubproduct.Commands;
using Manager.Application.Products.DeletePricePoint.Commands;
using Manager.Application.Products.DeleteProductMedia.Commands;
using Manager.Application.Products.GetProduct.Queries;
using Manager.Application.Products.GetProducts.Queries;
using Manager.Application.Products.PostPrice.Commands;
using Manager.Application.Products.RemoveSubproduct.Commands;
using Manager.Application.Products.SearchProducts.Queries;
using Manager.Application.Products.SetDescription.Commands;
using Manager.Application.Products.SetHoplink.Commands;
using Manager.Application.Products.SetMedia.Commands;
using Manager.Application.Products.SetRecurringPayment.Commands;
using Manager.Application.Products.SetShipping.Commands;
using Manager.Application.Products.UpdateMediaIndices.Commands;
using Manager.Application.Products.UpdateName.Commands;
using Manager.Application.Products.UpdatePrice.Commands;
using Manager.Application.Products.UpdatePricePoint.Commands;
using Manager.Application.Products.UpdateSubproduct.Commands;
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





        // ----------------------------------------------------------------------------- Delete Product Media --------------------------------------------------------------------
        [HttpDelete]
        [Route("Media")]
        public async Task<ActionResult> DeleteProductMedia(Guid id)
        {
            return SetResponse(await _mediator.Send(new DeleteProductMediaCommand(id)));
        }






        // ------------------------------------------------------------------------------ Delete Price Point ---------------------------------------------------------------------
        [HttpDelete]
        [Route("PricePoint")]
        public async Task<ActionResult> DeletePricePoint(string productId, Guid pricePointId)
        {
            return SetResponse(await _mediator.Send(new DeletePricePointCommand(productId, pricePointId)));
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




        // --------------------------------------------------------------------------------- Post Price --------------------------------------------------------------------------
        [HttpPost]
        [Route("Price")]
        public async Task<ActionResult> PostPrice(PostPriceCommand postPrice)
        {
            return SetResponse(await _mediator.Send(postPrice));
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







        // --------------------------------------------------------------------------------- Set Media ---------------------------------------------------------------------------
        [HttpPut]
        [Route("Media")]
        public async Task<ActionResult> SetMedia(SetMediaCommand setMedia)
        {
            return SetResponse(await _mediator.Send(setMedia));
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




        // ---------------------------------------------------------------------------- Update Media Indices ---------------------------------------------------------------------
        [HttpPut]
        [Route("MediaIndices")]
        public async Task<ActionResult> UpdateMediaIndices(UpdateMediaIndicesCommand updateMediaIndices)
        {
            return SetResponse(await _mediator.Send(updateMediaIndices));
        }




        // -------------------------------------------------------------------------------- Update Name --------------------------------------------------------------------------
        [HttpPut]
        public async Task<ActionResult> UpdateName(UpdateNameCommand updateName)
        {
            return SetResponse(await _mediator.Send(updateName));
        }




        // -------------------------------------------------------------------------------- Update Price -------------------------------------------------------------------------
        [HttpPut]
        [Route("Price")]
        public async Task<ActionResult> UpdatePrice(UpdatePriceCommand updatePrice)
        {
            return SetResponse(await _mediator.Send(updatePrice));
        }





        // ----------------------------------------------------------------------------- Update Price Point ----------------------------------------------------------------------
        [HttpPut]
        [Route("PricePoint")]
        public async Task<ActionResult> UpdatePricePoint(UpdatePricePointCommand updatePricePoint)
        {
            return SetResponse(await _mediator.Send(updatePricePoint));
        }




        // ----------------------------------------------------------------------------- Update Subproduct -----------------------------------------------------------------------
        [HttpPut]
        [Route("Subproduct")]
        public async Task<ActionResult> UpdateSubproduct(UpdateSubproductCommand updateSubproduct)
        {
            return SetResponse(await _mediator.Send(updateSubproduct));
        }
    }
}