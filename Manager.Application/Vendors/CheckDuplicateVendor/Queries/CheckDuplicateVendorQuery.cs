using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Vendors.CheckDuplicateVendor.Queries
{
    public sealed record CheckDuplicateVendorQuery(string VendorName) : IRequest<Result>;
}