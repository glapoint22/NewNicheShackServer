using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;
using Website.Application.Lists.AddCollaborator.Commands;
using Website.Application.Lists.AddProduct.Commands;
using Website.Application.Lists.CreateList.Commands;
using Website.Application.Lists.DeleteList.Commands;
using Website.Application.Lists.EditList.Commands;
using Website.Application.Lists.GetCollaborators.Queries;
using Website.Application.Lists.GetDropdownLists.Queries;
using Website.Application.Lists.GetListInfo.Queries;
using Website.Application.Lists.GetListProducts.Queries;
using Website.Application.Lists.GetLists.Queries;
using Website.Application.Lists.GetSharedList.Queries;
using Website.Application.Lists.MoveProduct.Commands;
using Website.Application.Lists.RemoveProduct.Commands;
using Website.Application.Lists.UpdateCollaborators.Commands;

namespace Website.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ListsController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public ListsController(ISender mediator)
        {
            _mediator = mediator;
        }





        // --------------------------------------------------------------------- Add Collaborator ----------------------------------------------------------------------
        [HttpPost]
        [Route("AddCollaborator")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> AddCollaborator(AddCollaboratorCommand addCollaborator)
        {
            return SetResponse(await _mediator.Send(addCollaborator));
        }






        // ----------------------------------------------------------------------- Add Product -------------------------------------------------------------------------
        [HttpPost]
        [Route("AddProduct")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> AddProduct(AddProductCommand addProduct)
        {
            return SetResponse(await _mediator.Send(addProduct));
        }





        // ----------------------------------------------------------------------- Create List -------------------------------------------------------------------------
        [HttpPost]
        [Route("CreateList")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> CreateList(CreateListCommand createList)
        {
            return SetResponse(await _mediator.Send(createList));
        }



        // ----------------------------------------------------------------------- Delete List -------------------------------------------------------------------------
        [HttpDelete]
        [Route("DeleteList")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> DeleteList(string listId)
        {
            return SetResponse(await _mediator.Send(new DeleteListCommand(listId)));
        }





        // ------------------------------------------------------------------------ Edit List --------------------------------------------------------------------------
        [HttpPut]
        [Route("EditList")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> EditList(EditListCommand editList)
        {
            return SetResponse(await _mediator.Send(editList));
        }










        // -------------------------------------------------------------------- Get Collaborators ----------------------------------------------------------------------
        [HttpGet]
        [Route("GetCollaborators")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> GetCollaborators(string listId)
        {
            return SetResponse(await _mediator.Send(new GetCollaboratorsQuery(listId)));
        }






        // ------------------------------------------------------------------- Get Dropdown Lists ----------------------------------------------------------------------
        [HttpGet]
        [Route("GetDropdownLists")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> GetDropdownLists()
        {
            return SetResponse(await _mediator.Send(new GetDropdownListsQuery()));
        }






        // --------------------------------------------------------------------- Get List Products ---------------------------------------------------------------------
        [HttpGet]
        [Route("GetListProducts")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> GetListProducts(string listId, string? sort)
        {
            return SetResponse(await _mediator.Send(new GetListProductsQuery(listId, sort)));
        }







        // ----------------------------------------------------------------------- Get Lists ----------------------------------------------------------------------------
        [HttpGet]
        [Route("GetLists")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> GetLists(string? listId, string? sort)
        {
            return SetResponse(await _mediator.Send(new GetListsQuery(listId, sort)));
        }







        // ---------------------------------------------------------------------- Get List Info -------------------------------------------------------------------------
        [HttpGet]
        [Route("ListInfo")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> GetListInfo(string collaborateId)
        {
            return SetResponse(await _mediator.Send(new GetListInfoQuery(collaborateId)));
        }






        // --------------------------------------------------------------------- Get Shared List ------------------------------------------------------------------------
        [HttpGet]
        [Route("GetSharedList")]
        public async Task<ActionResult> GetSharedList(string listId, string? sort)
        {
            return SetResponse(await _mediator.Send(new GetSharedListQuery(listId, sort)));
        }







        // ------------------------------------------------------------------ Get Shared List Products -----------------------------------------------------------------
        [HttpGet]
        [Route("GetSharedListProducts")]
        public async Task<ActionResult> GetSharedListProducts(string listId, string? sort)
        {
            return SetResponse(await _mediator.Send(new GetListProductsQuery(listId, sort)));
        }










        // ----------------------------------------------------------------------- Move Product -------------------------------------------------------------------------
        [HttpPut]
        [Route("MoveProduct")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> MoveProduct(MoveProductCommand moveProduct)
        {
            return SetResponse(await _mediator.Send(moveProduct));
        }








        // ---------------------------------------------------------------------- Remove Product -----------------------------------------------------------------------
        [HttpDelete]
        [Route("RemoveProduct")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> RemoveProduct(string productId, string listId)
        {
            return SetResponse(await _mediator.Send(new RemoveProductCommand(productId, listId)));
        }









        // ------------------------------------------------------------------ Update Collaborators ---------------------------------------------------------------------
        [HttpPut]
        [Route("UpdateCollaborators")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> UpdateCollaborators(UpdateCollaboratorsCommand updateCollaborators)
        {
            return SetResponse(await _mediator.Send(updateCollaborators));
        }
    }
}