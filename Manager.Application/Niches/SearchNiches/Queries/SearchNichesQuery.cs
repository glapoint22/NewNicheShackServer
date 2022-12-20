using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Niches.SearchNiches.Queries
{
    public sealed record SearchNichesQuery(string SearchTerm) : IRequest<Result>;
}