using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.AddCollaborator.Commands
{
    public sealed record AddCollaboratorCommand(string CollaborateId) : IRequest<Result>;
}