using Microsoft.AspNetCore.Mvc;
using Shared.Common.Classes;

namespace Shared.Common.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        public ActionResult SetResponse(Result result)
        {
            if (result.Success)
            {
                // Return the content
                if (result.ObjContent != null)
                {
                    return Ok(result.ObjContent);
                }
                else if (result.IntContent != null)
                {
                    return Ok(result.IntContent);
                }
                else if (result.StrContent != null)
                {
                    return Ok(result.StrContent);
                }
                else if (result.DblContent != null)
                {
                    return Ok(result.DblContent);
                }

                return NoContent();
            }
            else
            {
                foreach (var failure in result.Failures)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }


                // Return the error result
                if (result.Failures.Count > 0)
                {
                    string errorCode = result.Failures.First().ErrorCode;

                    if (errorCode == "401")
                    {
                        return Unauthorized(ModelState);
                    }
                    else if (errorCode == "404")
                    {
                        return NotFound(ModelState);
                    }
                    else if (errorCode == "409")
                    {
                        return Conflict(ModelState);
                    }
                }

                return BadRequest(ModelState);
            }
        }
    }
}