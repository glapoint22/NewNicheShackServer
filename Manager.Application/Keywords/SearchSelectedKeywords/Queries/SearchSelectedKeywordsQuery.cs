using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.SearchSelectedKeywords.Queries
{
    public sealed record SearchSelectedKeywordsQuery(Guid ProductId, string SearchTerm) : IRequest<Result>;
}