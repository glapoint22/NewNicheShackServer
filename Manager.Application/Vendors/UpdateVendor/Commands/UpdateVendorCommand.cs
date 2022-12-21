using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Vendors.UpdateVendor.Commands
{
    public sealed record UpdateVendorCommand(Guid Id, string Name, string? PrimaryEmail, string? PrimaryFirstName, string? PrimaryLastName) : IRequest<Result>;
}
