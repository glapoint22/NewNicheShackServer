using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Vendors.AddVendor.Commands
{
    public sealed record AddVendorCommand(string Name, string? PrimaryEmail, string? PrimaryFirstName, string? PrimaryLastName) : IRequest<Result>;
}