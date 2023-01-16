using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.MoveSubniche.Commands
{
    public sealed record MoveSubnicheCommand(Guid ItemToBeMovedId, Guid DestinationItemId) : IRequest<Result>;
}