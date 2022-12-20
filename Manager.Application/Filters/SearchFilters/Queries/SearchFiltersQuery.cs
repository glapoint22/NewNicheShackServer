using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.SearchFilters.Queries
{
    public sealed record SearchFiltersQuery(Guid? ProductId, string SearchTerm) : IRequest<Result>;
}