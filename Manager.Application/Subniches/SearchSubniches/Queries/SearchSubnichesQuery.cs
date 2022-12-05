using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.SearchSubniches.Queries
{
    public sealed record SearchSubnichesQuery(string SearchTerm) : IRequest<Result>;
}