using Manager.Application.ProductGroups.AddProductGroup.Commands;
using Manager.Application.ProductGroups.DeleteProductGroup.Commands;
using Manager.Application.ProductGroups.GetProductGroups.Queries;
using Manager.Application.ProductGroups.SearchProductGroups.Queries;
using Manager.Application.ProductGroups.SetProductGroupName.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public class ProductGroupsController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public ProductGroupsController(ISender mediator)
        {
            _mediator = mediator;
        }




        // ------------------------------------------------------------------- Add Product Group -------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> AddProductGroup(AddProductGroupCommand addProductGroup)
        {
            return SetResponse(await _mediator.Send(addProductGroup));
        }






        // ----------------------------------------------------------------- Delete Product Group ------------------------------------------------------------------------
        [HttpDelete]
        public async Task<ActionResult> DeleteProductGroup(Guid id)
        {
            return SetResponse(await _mediator.Send(new DeleteProductGroupCommand(id)));
        }








        // -------------------------------------------------------------------- Get Product Groups -----------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetProductGroups(string? productId)
        {
            return SetResponse(await _mediator.Send(new GetProductGroupsQuery(productId)));
        }





        // ----------------------------------------------------------------- Search Product Groups -----------------------------------------------------------------------
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult> SearchProductGroups(string? productId, string searchTerm)
        {
            return SetResponse(await _mediator.Send(new SearchProductGroupsQuery(productId, searchTerm)));
        }








        // ----------------------------------------------------------------- Set Product Group Name ----------------------------------------------------------------------
        [HttpPut]
        public async Task<ActionResult> SetProductGroupName(SetProductGroupNameCommand setProductGroupName)
        {
            return SetResponse(await _mediator.Send(setProductGroupName));
        }
    }
}
