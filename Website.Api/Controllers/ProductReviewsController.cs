using MediatR;
using Microsoft.AspNetCore.Mvc;
using Website.Application.ProductReviews.Queries;

namespace Website.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public sealed class ProductReviewsController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public ProductReviewsController(ISender mediator)
        {
            _mediator = mediator;
        }


        // ---------------------------------------------------------------------- Get Reviews --------------------------------------------------------------------------
        [HttpGet]
        [Route("GetReviews")]
        public async Task<ActionResult> GetReviews(string productId, int page, string sortBy, string filterBy)
        {
            return SetResponse(await _mediator.Send(new GetReviewsQuery(productId, page, sortBy, filterBy)));
        }


    }
}