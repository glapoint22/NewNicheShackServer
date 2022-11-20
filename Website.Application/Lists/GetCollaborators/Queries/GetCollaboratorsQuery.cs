using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Lists.GetCollaborators.Queries
{
    public sealed record GetCollaboratorsQuery(string ListId) : IRequest<Result>;
}