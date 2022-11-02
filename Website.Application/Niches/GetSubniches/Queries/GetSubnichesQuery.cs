using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Niches.GetSubniches.Queries
{
    public sealed record GetSubnichesQuery(string NicheId) : IRequest<Result>;
}