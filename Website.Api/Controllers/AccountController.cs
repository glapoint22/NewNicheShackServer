using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Application.Account.ActivateAccount.Commands;
using Website.Application.Account.CreateDeleteAccountOTP.Commands;
using Website.Application.Account.DeleteRefreshToken.Commands;
using Website.Application.Account.ExternalLogIn.Commands;
using Website.Application.Account.LogIn.Commands;
using Website.Application.Account.LogOut.Commands;
using Website.Application.Account.Refresh.Commands;
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




        // --------------------------------------------------------------- Create Delete Account OTP ------------------------------------------------------------------
        [HttpGet]
        [Route("CreateDeleteAccountOTP")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> CreateDeleteAccountOTP()
        {
            return SetResponse(await _mediator.Send(new CreateDeleteAccountOTPCommand()));
        }




        // ------------------------------------------------------------------ Delete Refresh Token --------------------------------------------------------------------
        [HttpDelete]
        [Route("Refresh")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> DeleteRefreshToken(string newRefreshToken)
        {
            return SetResponse(await _mediator.Send(new DeleteRefreshTokenCommand(newRefreshToken)));
        }



        
        // --------------------------------------------------------------------- External LogIn -----------------------------------------------------------------------
        [HttpPost]
        [Route("ExternalLogIn")]
        public async Task<ActionResult> ExternalLogIn(ExternalLogInCommand externalLogIn)
        {
            return SetResponse(await _mediator.Send(externalLogIn));
        }






        // ------------------------------------------------------------------------- Log In ---------------------------------------------------------------------------
        [HttpPost]
        [Route("LogIn")]
        public async Task<ActionResult> LogIn(LogInCommand logIn)
        {
            return SetResponse(await _mediator.Send(logIn));
        }



        // ------------------------------------------------------------------------- Log Out --------------------------------------------------------------------------
        [HttpGet]
        [Route("LogOut")]
        public async Task<ActionResult> LogOut()
        {
            return SetResponse(await _mediator.Send(new LogOutCommand()));
        }




        // ------------------------------------------------------------------------- Refresh --------------------------------------------------------------------------
        [HttpGet]
        [Route("Refresh")]
        public async Task<ActionResult> Refresh()
        {
            return SetResponse(await _mediator.Send(new RefreshCommand()));
        }





        // -------------------------------------------------------------- Resend Account Activation Email -------------------------------------------------------------
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