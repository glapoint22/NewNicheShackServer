using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Application.Lists.AddCollaborator.Commands;
using Website.Application.Lists.AddProduct.Commands;
using Website.Application.Lists.CollaboratorProducts.Queries;
using Website.Application.Lists.CreateList.Commands;
using Website.Application.Lists.DeleteList.Commands;
using Website.Application.Lists.DropdownLists.Queries;
using Website.Application.Lists.EditList.Commands;
using Website.Application.Lists.GetCollaborators.Queries;
using Website.Application.Lists.GetListInfo.Queries;
using Website.Application.Lists.ListCollection.Queries;
using Website.Application.Lists.MoveProduct.Commands;
using Website.Application.Lists.RemoveProduct.Commands;
using Website.Application.Lists.SharedList.Queries;
using Website.Application.Lists.UpdateCollaborators.Commands;

namespace Website.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ListsController : ApiControllerBase
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
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> CreateList(CreateListCommand createList)
        {
            return SetResponse(await _mediator.Send(createList));
        }



        // ----------------------------------------------------------------------- Delete List -------------------------------------------------------------------------
        [HttpDelete]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> DeleteList(string listId)
        {
            return SetResponse(await _mediator.Send(new DeleteListCommand(listId)));
        }





        // ------------------------------------------------------------------------ Edit List --------------------------------------------------------------------------
        [HttpPut]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> EditList(EditListCommand editList)
        {
            return SetResponse(await _mediator.Send(editList));
        }








        // ---------------------------------------------------------------- Get Collaborators Products -----------------------------------------------------------------
        [HttpGet]
        [Route("Products")]
        public async Task<ActionResult> GetCollaboratorProducts(string listId)
        {
            return SetResponse(await _mediator.Send(new GetCollaboratorProductsQuery(listId)));
        }





        // -------------------------------------------------------------------- Get Collaborators ----------------------------------------------------------------------
        [HttpGet]
        [Route("Collaborators")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> GetCollaborators(string listId)
        {
            return SetResponse(await _mediator.Send(new GetCollaboratorsQuery(listId)));
        }






        // ------------------------------------------------------------------- Get Dropdown Lists ----------------------------------------------------------------------
        [HttpGet]
        [Route("DropdownLists")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> GetDropdownLists()
        {
            return SetResponse(await _mediator.Send(new GetDropdownListsQuery()));
        }





        // ------------------------------------------------------------------- Get List Collection ----------------------------------------------------------------------
        [HttpGet]
        [Route("ListCollection")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> GetListCollection()
        {
            return SetResponse(await _mediator.Send(new GetListCollectionQuery()));
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
        [Route("SharedList")]
        public async Task<ActionResult> GetSharedList(string listId, string sort)
        {
            return SetResponse(await _mediator.Send(new GetSharedListQuery(listId, sort)));
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
        public async Task<ActionResult> RemoveProduct(Guid collaboratorProductId)
        {
            return SetResponse(await _mediator.Send(new RemoveProductCommand(collaboratorProductId)));
        }









        // ------------------------------------------------------------------ Update Collaborators ---------------------------------------------------------------------
        [HttpPut]
        [Route("Collaborators")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> UpdateCollaborators(UpdateCollaboratorsCommand updateCollaborators)
        {
            return SetResponse(await _mediator.Send(updateCollaborators));
        }
    }
}