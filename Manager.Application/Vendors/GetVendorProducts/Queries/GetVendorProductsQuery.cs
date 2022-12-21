using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Vendors.GetVendorProducts.Queries
{
    public sealed record GetVendorProductsQuery(Guid Id) : IRequest<Result>;
}