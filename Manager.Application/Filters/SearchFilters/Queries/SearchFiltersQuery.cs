using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.SearchFilters.Queries
{
    public sealed record SearchFiltersQuery(string? ProductId, string SearchTerm) : IRequest<Result>;
}