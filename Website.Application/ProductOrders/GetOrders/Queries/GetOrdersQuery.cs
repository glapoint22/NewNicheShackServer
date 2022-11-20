using MediatR;
using Shared.Common.Classes;

namespace Website.Application.ProductOrders.GetOrders.Queries
{
    public sealed record GetOrdersQuery(string? Filter, string? SearchTerm = null) : IRequest<Result>;
}