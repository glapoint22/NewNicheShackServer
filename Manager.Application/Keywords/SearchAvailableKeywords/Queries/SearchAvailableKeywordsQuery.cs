using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Keywords.SearchAvailableKeywords.Queries
{
    public sealed record SearchAvailableKeywordsQuery(string ProductId, string SearchTerm) : IRequest<Result>;
}