using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.GetPageKeywordGroups.Queries
{
    public sealed record GetPageKeywordGroupsQuery(Guid PageId) : IRequest<Result>;
}