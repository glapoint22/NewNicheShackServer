using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Vendors.DeleteVendor.Commands
{
    public sealed record DeleteVendorCommand(Guid Id) : IRequest<Result>;
}