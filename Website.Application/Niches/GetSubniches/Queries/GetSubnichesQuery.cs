using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Niches.GetSubniches.Queries
{
    public sealed record GetSubnichesQuery(string NicheId) : IRequest<Result>;
}