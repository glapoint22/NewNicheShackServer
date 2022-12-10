using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.SetVendor.Commands
{
    public sealed record SetVendorCommand(string ProductId, Guid VendorId) : IRequest<Result>;
}