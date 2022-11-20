using MediatR;
using Shared.Common.Classes;

namespace Website.Application.ProductOrders.GetOrderFilters.Queries
{
    public sealed record GetOrderFiltersQuery() : IRequest<Result>;
}