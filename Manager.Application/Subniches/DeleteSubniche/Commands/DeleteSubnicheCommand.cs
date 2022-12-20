using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Subniches.DeleteSubniche.Commands
{
    public sealed record DeleteSubnicheCommand(Guid Id) : IRequest<Result>;
}