using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Niches.DisableEnableNiche.Commands
{
    public sealed record DisableEnableNicheCommand(Guid NicheId) : IRequest<Result>;
}