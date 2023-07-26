using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.SearchLinkSubniches.Queries
{
    public sealed record SearchLinkSubnichesQuery(string SearchTerm) : IRequest<Result>;
}