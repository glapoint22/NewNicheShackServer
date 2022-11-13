using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Application.ProductOrders.GetOrderFilters.Queries;
using Website.Application.ProductOrders.GetOrders.Queries;
using Website.Application.ProductOrders.PostOrder.Commands;

namespace Website.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ProductOrdersController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public ProductOrdersController(ISender mediator)
        {
            _mediator = mediator;
        }


        // -------------------------------------------------------------------- Get Order Filters ----------------------------------------------------------------------------
        [HttpGet]
        [Route("GetOrderFilters")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> GetOrderFilters()
        {
            return SetResponse(await _mediator.Send(new GetOrderFiltersQuery()));
        }




        // ------------------------------------------------------------------------ Get Orders -------------------------------------------------------------------------------
        [HttpGet]
        [Route("GetOrders")]
        [Authorize(Policy = "Account")]
        public async Task<ActionResult> GetOrders(string? filter, string? orderSearch = null)
        {
            return SetResponse(await _mediator.Send(new GetOrdersQuery(filter, orderSearch)));
        }





        // ------------------------------------------------------------------------ Post Order -------------------------------------------------------------------------------
        [HttpPost]
        [Route("PostOrder")]
        public async Task<ActionResult> PostOrder(PostOrderCommand postOrder)
        {
            return SetResponse(await _mediator.Send(postOrder));
        }
    }
}
