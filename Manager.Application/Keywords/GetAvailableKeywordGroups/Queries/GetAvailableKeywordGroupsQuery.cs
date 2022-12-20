using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.GetAvailableKeywordGroups.Queries
{
    public sealed record GetAvailableKeywordGroupsQuery(Guid? ProductId) : IRequest<Result>;
}