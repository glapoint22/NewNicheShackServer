using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Controllers;
using Website.Application.ProductReviews.GetPositiveNegativeReviews.Queries;
using Website.Application.ProductReviews.GetReviews.Queries;
using Website.Application.ProductReviews.PostReview.Commands;
using Website.Application.ProductReviews.RateReview.Commands;

namespace Website.Api.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult> GetReviews(Guid productId, string? sortBy, string? filterBy, int page = 1)
        {
            return SetResponse(await _mediator.Send(new GetReviewsQuery(productId, page, sortBy, filterBy)));
        }






        // -------------------------------------------------------------- Get Positive Negative Reviews ----------------------------------------------------------------
        [HttpGet]
        [Route("GetPositiveNegativeReviews")]
        public async Task<ActionResult> GetPositiveNegativeReviews(Guid productId)
        {
            return SetResponse(await _mediator.Send(new GetPositiveNegativeReviewsQuery(productId)));
        }





        // --------------------------------------------------------------------- Post Review ---------------------------------------------------------------------------
        [HttpPost]
        [Route("PostReview")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> PostReview(PostReviewCommand postReview)
        {
            return SetResponse(await _mediator.Send(postReview));
        }





        // --------------------------------------------------------------------- Rate Review ---------------------------------------------------------------------------
        [HttpPut]
        [Route("RateReview")]
        public async Task<ActionResult> RateReview(RateReviewCommand rateReview)
        {
            return SetResponse(await _mediator.Send(rateReview));
        }
    }
}