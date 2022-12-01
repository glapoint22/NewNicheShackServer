using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.QueryBuilders.GetQueryLists.Queries
{
    public sealed record GetQueryListsQuery() : IRequest<Result>;
}