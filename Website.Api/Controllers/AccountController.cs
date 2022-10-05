using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [Route("SignUp")]
        public async Task<ActionResult> SignUp(SignUpCommand signUp)
        {
            return SetResponse(await _mediator.Send(signUp));
        }
    }
}