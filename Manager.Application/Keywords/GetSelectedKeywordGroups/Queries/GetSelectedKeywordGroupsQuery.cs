using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.GetSelectedKeywordGroups.Queries
{
    public sealed record GetSelectedKeywordGroupsQuery(Guid ProductId) : IRequest<Result>;
}