using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.RemoveSubproduct.Commands
{
    public sealed record RemoveSubproductCommand(Guid SubproductId) : IRequest<Result>;
}