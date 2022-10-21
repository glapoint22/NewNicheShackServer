using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.ProductOrders.GetOrders.Queries
{
    public sealed class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Result>
    {
        public Task<Result> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}