using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.GetAvailableKeywordGroups.Queries
{
    public sealed record GetAvailableKeywordGroupsQuery(string? ProductId) : IRequest<Result>;
}