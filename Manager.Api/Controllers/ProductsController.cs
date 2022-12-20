using Manager.Application.Products.AddPricePoint.Commands;
using Manager.Application.Products.AddProduct.Commands;
using Manager.Application.Products.AddSubproduct.Commands;
using Manager.Application.Products.DeleteProduct.Commands;
using Manager.Application.Products.GetProduct.Queries;
using Manager.Application.Products.GetProductParent.Queries;
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
using Manager.Application.Products.SetProductFilter.Commands;
using Manager.Application.Products.SetProductGroup.Commands;
using Manager.Application.Products.SetProductKeyword.Commands;
using Manager.Application.Products.SetProductMedia.Commands;
using Manager.Application.Products.SetProductMediaIndices.Commands;
using Manager.Application.Products.SetRecurringPayment.Commands;
using Manager.Application.Products.SetShipping.Commands;
using Manager.Application.Products.SetSubproduct.Commands;
using Manager.Application.Products.SetVendor.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public sealed class ProductsController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public ProductsController(ISender mediator)
        {
            _mediator = mediator;
        }




        // --------------------------------------------------------------------------------- Add Product -------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> AddProduct(AddProductCommand addProduct)
        {
            return SetResponse(await _mediator.Send(addProduct));
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







        // ------------------------------------------------------------------------------- Delete Product ------------------------------------------------------------------------
        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            return SetResponse(await _mediator.Send(new DeleteProductCommand(id)));
        }






        // --------------------------------------------------------------------------------- Get Product -------------------------------------------------------------------------
        [HttpGet]
        [Route("Product")]
        public async Task<ActionResult> GetProduct(Guid productId)
        {
            return SetResponse(await _mediator.Send(new GetProductQuery(productId)));
        }








        // ------------------------------------------------------------------------------ Get Product Parent ---------------------------------------------------------------------
        [HttpGet]
        [Route("Parent")]
        public async Task<ActionResult> GetProductParent(Guid productId)
        {
            return SetResponse(await _mediator.Send(new GetProductParentQuery(productId)));
        }







        // -------------------------------------------------------------------------------- Get Products -------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetProducts(Guid parentId)
        {
            return SetResponse(await _mediator.Send(new GetProductsQuery(parentId)));
        }






        // ------------------------------------------------------------------------------ Remove Price Point ---------------------------------------------------------------------
        [HttpDelete]
        [Route("PricePoint")]
        public async Task<ActionResult> RemovePricePoint(Guid productId, Guid pricePointId)
        {
            return SetResponse(await _mediator.Send(new RemovePricePointCommand(productId, pricePointId)));
        }






        // ----------------------------------------------------------------------------- Remove Product Media --------------------------------------------------------------------
        [HttpDelete]
        [Route("Media")]
        public async Task<ActionResult> RemoveProductMedia(Guid productId, Guid productMediaId)
        {
            return SetResponse(await _mediator.Send(new RemoveProductMediaCommand(productId, productMediaId)));
        }






        // ------------------------------------------------------------------------------ Remove Subproduct ----------------------------------------------------------------------
        [HttpDelete]
        [Route("Subproduct")]
        public async Task<ActionResult> RemoveSubproduct(Guid subproductId)
        {
            return SetResponse(await _mediator.Send(new RemoveSubproductCommand(subproductId)));
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









        // ----------------------------------------------------------------------------- Set Product Filter ----------------------------------------------------------------------
        [HttpPut]
        [Route("Filter")]
        public async Task<ActionResult> SetProductFilter(SetProductFilterCommand setProductFilter)
        {
            return SetResponse(await _mediator.Send(setProductFilter));
        }












        // ----------------------------------------------------------------------------- Set Product Group -----------------------------------------------------------------------
        [HttpPut]
        [Route("ProductGroup")]
        public async Task<ActionResult> SetProductGroup(SetProductGroupCommand setProductGroup)
        {
            return SetResponse(await _mediator.Send(setProductGroup));
        }








        // ----------------------------------------------------------------------------- Set Product Keyword ---------------------------------------------------------------------
        [HttpPut]
        [Route("Keyword")]
        public async Task<ActionResult> SetProductKeyword(SetProductKeywordCommand setProductKeyword)
        {
            return SetResponse(await _mediator.Send(setProductKeyword));
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









        // --------------------------------------------------------------------------------- Set Vendor --------------------------------------------------------------------------
        [HttpPut]
        [Route("Vendor")]
        public async Task<ActionResult> SetVendor(SetVendorCommand setVendor)
        {
            return SetResponse(await _mediator.Send(setVendor));
        }
    }
}