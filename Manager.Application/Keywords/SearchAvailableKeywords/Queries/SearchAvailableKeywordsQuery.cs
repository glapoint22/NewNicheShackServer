using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.SearchAvailableKeywords.Queries
{
    public sealed record SearchAvailableKeywordsQuery(Guid ProductId, string SearchTerm) : IRequest<Result>;
}