using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.DisableEnableSubniche.Commands
{
    public sealed record DisableEnableSubnicheCommand(Guid SubnicheId) : IRequest<Result>;
}