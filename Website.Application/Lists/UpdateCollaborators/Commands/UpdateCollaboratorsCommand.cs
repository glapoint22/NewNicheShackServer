using MediatR;
using Shared.Common.Classes;
using Website.Application.Lists.UpdateCollaborators.Classes;

namespace Website.Application.Lists.UpdateCollaborators.Commands
{
    public sealed record UpdateCollaboratorsCommand(List<UpdatedCollaborator> UpdatedCollaborators, string ListId) : IRequest<Result>;
}