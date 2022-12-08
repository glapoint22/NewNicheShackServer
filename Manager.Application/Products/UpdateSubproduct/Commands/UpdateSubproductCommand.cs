using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.UpdateSubproduct.Commands
{
    public sealed record UpdateSubproductCommand(string ProductId, Guid SubproductId, string? Name, string? Description, Guid? ImageId, double Value) : IRequest<Result>;
}