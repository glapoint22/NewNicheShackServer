using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Products.MoveProduct.Commands
{
    public sealed record MoveProductCommand(Guid ItemToBeMovedId, Guid DestinationItemId) : IRequest<Result>;
}