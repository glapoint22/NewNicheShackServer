using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Vendors.GetVendor.Queries
{
    public sealed record GetVendorQuery(Guid Id) : IRequest<Result>;
}