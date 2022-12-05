using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Pages.GetPageKeywords.Queries
{
    public sealed record GetPageKeywordsQuery(string PageId, Guid KeywordGroupId) : IRequest<Result>;
}