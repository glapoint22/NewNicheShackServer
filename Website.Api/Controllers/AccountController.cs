using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Application.Account.ActivateAccount.Commands;
using Website.Application.Account.AddPassword.Commands;
using Website.Application.Account.ChangeEmail.Commands;
using Website.Application.Account.ChangeName.Commands;
using Website.Application.Account.ChangePassword.Commands;
using Website.Application.Account.ChangeProfileImage.Commands;
using Website.Application.Account.CreateChangeEmailOTP.Commands;
using Website.Application.Account.CreateDeleteAccountOTP.Commands;
using Website.Application.Account.DeleteAccount.Commands;
using Website.Application.Account.DeleteRefreshToken.Commands;
using Website.Application.Account.ExternalLogIn.Commands;
using Website.Application.Account.ForgotPassword.Commands;
using Website.Application.Account.IsDuplicateEmail.Queries;
using Website.Application.Account.LogIn.Commands;
using Website.Application.Account.LogOut.Commands;
using Website.Application.Account.Refresh.Commands;
using Website.Application.Account.ResendAccountActivationEmail.Commands;
using Website.Application.Account.ResetPassword.Commands;
using Website.Application.Account.SignUp.Commands;

namespace Website.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class AccountController : ApiControllerBase
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




        // ----------------------------------------------------------------------- Add Password -----------------------------------------------------------------------
        [HttpGet]
        [Route("AddPassword")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> AddPassword(string password)
        {
            return SetResponse(await _mediator.Send(new AddPasswordCommand(password)));
        }




        // ------------------------------------------------------------------------ Change Email ------------------------------------------------------------------------
        [HttpPut]
        [Route("ChangeEmail")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> ChangeEmail(ChangeEmailCommand changeEmail)
        {
            return SetResponse(await _mediator.Send(changeEmail));
        }





        // ------------------------------------------------------------------------ Change Name -------------------------------------------------------------------------
        [HttpPut]
        [Route("ChangeName")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> ChangeName(ChangeNameCommand changeName)
        {
            return SetResponse(await _mediator.Send(changeName));
        }






        // ---------------------------------------------------------------------- Change Password -----------------------------------------------------------------------
        [HttpPut]
        [Route("ChangePassword")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> ChangePassword(ChangePasswordCommand changePassword)
        {
            return SetResponse(await _mediator.Send(changePassword));
        }





        // ------------------------------------------------------------------- Change Profile Image ---------------------------------------------------------------------
        [HttpPost]
        [Route("ChangeProfileImage")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> ChangeProfileImage()
        {
            return SetResponse(await _mediator.Send(new ChangeProfileImageCommand()));
        }






        // ----------------------------------------------------------------- Create Change Email OTP ------------------------------------------------------------------
        [HttpGet]
        [Route("CreateChangeEmailOTP")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> CreateChangeEmailOTP(string email)
        {
            return SetResponse(await _mediator.Send(new CreateChangeEmailOTPCommand(email)));
        }






        // ---------------------------------------------------------------- Create Delete Account OTP -----------------------------------------------------------------
        [HttpGet]
        [Route("CreateDeleteAccountOTP")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> CreateDeleteAccountOTP()
        {
            return SetResponse(await _mediator.Send(new CreateDeleteAccountOTPCommand()));
        }



        // --------------------------------------------------------------------- Delete Account -----------------------------------------------------------------------
        [HttpDelete]
        [Route("DeleteAccount")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> DeleteAccount(string token, string password)
        {
            return SetResponse(await _mediator.Send(new DeleteAccountCommand(token, password)));
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




        // ---------------------------------------------------------------------- Forgot Password -----------------------------------------------------------------------
        [HttpGet]
        [Route("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(string email)
        {
            return SetResponse(await _mediator.Send(new ForgotPasswordCommand(email)));
        }




        // -------------------------------------------------------------------- Is Duplicate Email ----------------------------------------------------------------------
        [HttpGet]
        [Route("IsDuplicateEmail")]
        public async Task<ActionResult> IsDuplicateEmail(string email)
        {
            return Ok(await _mediator.Send(new IsDuplicateEmailQuery(email)));
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
        [Authorize(Policy = "Account")]
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





        // ---------------------------------------------------------------------- Reset Password ----------------------------------------------------------------------
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordCommand resetPassword)
        {
            return SetResponse(await _mediator.Send(resetPassword));
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