using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.SearchKeywordGroups.Queries
{
    public sealed record SearchKeywordGroupsQuery(string SearchTerm) : IRequest<Result>;
}