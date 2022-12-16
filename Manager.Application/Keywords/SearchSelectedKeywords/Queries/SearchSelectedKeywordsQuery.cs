using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.SearchSelectedKeywords.Queries
{
    public sealed record SearchSelectedKeywordsQuery(string ProductId, string SearchTerm) : IRequest<Result>;
}