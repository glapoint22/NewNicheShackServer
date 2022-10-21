﻿using Microsoft.AspNetCore.Mvc;
using Website.Application.Common.Classes;

namespace Website.Api.Controllers
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
                string errorCode = result.Failures.First().ErrorCode;

                if (errorCode == "401")
                {
                    return Unauthorized(ModelState);
                }
                else if (errorCode == "404")
                {
                    return NotFound(ModelState);
                }

                return BadRequest(ModelState);
            }
        }
    }
}