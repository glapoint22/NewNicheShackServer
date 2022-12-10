using Manager.Application.Account.DeleteRefreshToken.Commands;
using Manager.Application.Account.LogIn;
using Manager.Application.Account.LogOut.Commands;
using Manager.Application.Account.Refresh.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
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



        // ------------------------------------------------------------------ Delete Refresh Token --------------------------------------------------------------------
        [HttpDelete]
        [Route("Refresh")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> DeleteRefreshToken(string newRefreshToken)
        {
            return SetResponse(await _mediator.Send(new DeleteRefreshTokenCommand(newRefreshToken)));
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
    }
}
