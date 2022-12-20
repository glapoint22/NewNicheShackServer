using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.AddSubniche.Commands
{
    public sealed record AddSubnicheCommand(Guid Id, string Name) : IRequest<Result>;
}