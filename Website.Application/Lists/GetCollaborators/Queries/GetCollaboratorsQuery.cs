using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.GetCollaborators.Queries
{
    public record GetCollaboratorsQuery(string ListId) : IRequest<Result>;
}