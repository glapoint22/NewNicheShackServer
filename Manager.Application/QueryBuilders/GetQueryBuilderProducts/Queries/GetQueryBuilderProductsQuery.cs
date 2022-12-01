using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.QueryBuilders.GetQueryBuilderProducts.Queries
{
    public sealed record GetQueryBuilderProductsQuery(string QueryString) : IRequest<Result>;
}