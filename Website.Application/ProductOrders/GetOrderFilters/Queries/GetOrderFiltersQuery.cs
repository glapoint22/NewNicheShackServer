using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.ProductOrders.GetOrderFilters.Queries
{
    public sealed record GetOrderFiltersQuery() : IRequest<Result>;
}