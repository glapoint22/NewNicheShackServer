using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.GetCollaborators.Queries
{
    public sealed record GetCollaboratorsQuery(string ListId) : IRequest<Result>;
}