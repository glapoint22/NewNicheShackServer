using MediatR;
using Microsoft.AspNetCore.Mvc;
using Website.Application.Account.ActivateAccount.Commands;
using Website.Application.Account.LogIn.Commands;
using Website.Application.Account.SignUp.Commands;

namespace Website.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public AccountController(ISender mediator)
        {
            _mediator = mediator;
        }

        
        // --------------------------------------------------------------------- Activate Account ---------------------------------------------------------------------
        [HttpPost]
        [Route("ActivateAccount")]
        public async Task<ActionResult> ActivateAccount(ActivateAccountCommand activateAccount)
        {
            return SetResponse(await _mediator.Send(activateAccount));
        }




        // ------------------------------------------------------------------------- Log In ---------------------------------------------------------------------------
        [HttpPost]
        [Route("LogIn")]
        public async Task<ActionResult> LogIn(LogInCommand logIn)
        {
            return SetResponse(await _mediator.Send(logIn));
        }




        
        // ------------------------------------------------------------- Resend Account Activation Email --------------------------------------------------------------
        [HttpGet]
        [Route("ResendAccountActivationEmail")]
        public async Task<ActionResult> ResendAccountActivationEmail(string email)
        {
            return SetResponse(await _mediator.Send(new ResendAccountActivationEmailCommand(email)));
        }



        // ------------------------------------------------------------------------- Sign Up --------------------------------------------------------------------------
        [HttpPost]
        [Route("SignUp")]
        public async Task<ActionResult> SignUp(SignUpCommand signUp)
        {
            return SetResponse(await _mediator.Send(signUp));
        }
    }
}