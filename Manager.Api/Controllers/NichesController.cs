using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class NichesController : ControllerBase
    {
        // ----------------------------------------------------------------------- Get Niches ----------------------------------------------------------------------------
        [HttpGet]
        [Route("GetNiches")]
        public async Task<ActionResult> GetNiches()
        {
            return Ok();
        }
    }
}
