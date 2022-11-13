using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.ProductOrders.GetOrders.Queries
{
    public sealed record GetOrdersQuery(string? Filter, string? SearchTerm = null) : IRequest<Result>;
}