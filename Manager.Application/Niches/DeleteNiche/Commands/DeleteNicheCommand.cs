using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Niches.DeleteNiche.Commands
{
    public sealed record DeleteNicheCommand(Guid Id) : IRequest<Result>;
}