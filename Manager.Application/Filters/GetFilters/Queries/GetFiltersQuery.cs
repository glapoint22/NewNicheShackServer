using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Filters.GetFilters.Queries
{
    public sealed record GetFiltersQuery() : IRequest<Result>;
}