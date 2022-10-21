using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Application.Account.ChangeEmail.Commands;
using Website.Application.EmailPreferences.Commands;
using Website.Application.EmailPreferences.Common;
using Website.Application.EmailPreferences.Queries;

namespace Website.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public sealed class EmailPreferencesController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public EmailPreferencesController(ISender mediator)
        {
            _mediator = mediator;
        }


        // ------------------------------------------------------------------- Get Email Preferences ------------------------------------------------------------------
        [HttpGet]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> GetEmailPreferences()
        {
            return Ok(await _mediator.Send(new GetEmailPreferencesQuery()));
        }





        // ------------------------------------------------------------------------ Set Email Preferences ------------------------------------------------------------------------
        [HttpPut]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> SetEmailPreferences(Preferences emailPreferences)
        {
            return SetResponse(await _mediator.Send(new SetEmailPreferencesCommand(emailPreferences)));
        }
    }
}