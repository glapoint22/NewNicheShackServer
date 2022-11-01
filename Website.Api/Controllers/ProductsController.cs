using MediatR;
using Microsoft.AspNetCore.Mvc;
using Website.Application.Products.GetProduct.Queries;

namespace Website.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public sealed class ProductsController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public ProductsController(ISender mediator)
        {
            _mediator = mediator;
        }

        // ---------------------------------------------------------------------- Get Product --------------------------------------------------------------------------
        [HttpGet]
        [Route("GetProduct")]
        public async Task<ActionResult> GetProduct(int productId)
        {
            return SetResponse(await _mediator.Send(new GetProductQuery(productId)));
        }
    }
}