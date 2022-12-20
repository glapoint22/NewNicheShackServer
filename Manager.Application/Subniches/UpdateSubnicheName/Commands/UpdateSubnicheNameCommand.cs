using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.UpdateSubnicheName.Commands
{
    public sealed record UpdateSubnicheNameCommand(Guid Id, string Name) : IRequest<Result>;
}