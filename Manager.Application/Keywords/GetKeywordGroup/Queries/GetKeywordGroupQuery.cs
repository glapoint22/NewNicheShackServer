using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.GetKeywordGroup.Queries
{
    public sealed record GetKeywordGroupQuery(Guid KeywordId) : IRequest<Result>;
}