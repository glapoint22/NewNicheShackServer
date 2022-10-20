using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.AddCollaborator.Commands
{
    public record AddCollaboratorCommand(string CollaborateId) : IRequest<Result>;
}