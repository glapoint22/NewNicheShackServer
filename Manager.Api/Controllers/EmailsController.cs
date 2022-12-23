using Manager.Application.Emails.DeleteEmail.Commands;
using Manager.Application.Emails.DuplicateEmail.Commands;
using Manager.Application.Emails.GetEmail.Queries;
using Manager.Application.Emails.NewEmail.Commands;
using Manager.Application.Emails.SearchEmails.Queries;
using Manager.Application.Emails.UpdateEmail.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Account")]
    [ApiController]
    public class EmailsController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public EmailsController(ISender mediator)
        {
            _mediator = mediator;
        }



        // ------------------------------------------------------------------------------- Delete Email ---------------------------------------------------------------------------
        [HttpDelete]
        public async Task<ActionResult> DeleteEmail(Guid pageId)
        {
            return SetResponse(await _mediator.Send(new DeleteEmailCommand(pageId)));
        }





        // ----------------------------------------------------------------------------- Duplicate Email -------------------------------------------------------------------------
        [HttpPost]
        [Route("Duplicate")]
        public async Task<ActionResult> DuplicateEmail(DuplicateEmailCommand duplicateEmail)
        {
            return SetResponse(await _mediator.Send(duplicateEmail));
        }





        // -------------------------------------------------------------------------------- Get Email ----------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetEmail(Guid id)
        {
            return SetResponse(await _mediator.Send(new GetEmailQuery(id)));
        }





        // -------------------------------------------------------------------------------- New Email ----------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> NewEmail(NewEmailCommand newEmail)
        {
            return SetResponse(await _mediator.Send(newEmail));
        }







        // ------------------------------------------------------------------------------ Search Emails --------------------------------------------------------------------------
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult> SearchEmails(string searchTerm)
        {
            return SetResponse(await _mediator.Send(new SearchEmailsQuery(searchTerm)));
        }




        // ------------------------------------------------------------------------------- Update Email --------------------------------------------------------------------------
        [HttpPut]
        public async Task<ActionResult> UpdateEmail(UpdateEmailCommand updateEmail)
        {
            return SetResponse(await _mediator.Send(updateEmail));
        }
    }
}