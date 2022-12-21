using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Niches.GetSubnichesProducts.Queries
{
    public sealed record GetSubnichesProductsQuery(Guid NicheId, Guid SubnicheId) : IRequest<Result>;
}