using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.RemoveSubproduct.Commands
{
    public sealed record RemoveSubproductCommand(string ProductId, Guid SubproductId) : IRequest<Result>;
}