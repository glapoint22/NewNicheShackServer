using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Lists.GetListProducts.Queries
{
    public sealed record GetListProductsQuery(string ListId, string? Sort = null) : IRequest<Result>;
}