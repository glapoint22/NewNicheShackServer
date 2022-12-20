using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Niches.AddNiche.Commands
{
    public sealed record AddNicheCommand(string Name) : IRequest<Result>;
}