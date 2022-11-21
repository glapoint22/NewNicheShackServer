using Manager.Application.Account.LogIn;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public AccountController(ISender mediator)
        {
            _mediator = mediator;
        }


        // ------------------------------------------------------------------------- Log In ---------------------------------------------------------------------------
        [HttpPost]
        [Route("LogIn")]
        public async Task<ActionResult> LogIn(LogInCommand logIn)
        {
            return SetResponse(await _mediator.Send(logIn));
        }
    }
}
