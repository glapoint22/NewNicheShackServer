using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Vendors.SearchVendors.Queries
{
    public sealed record SearchVendorsQuery(string SearchTerm) : IRequest<Result>;
}