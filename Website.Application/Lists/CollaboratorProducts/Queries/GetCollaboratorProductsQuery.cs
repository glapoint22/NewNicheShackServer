using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.CollaboratorProducts.Queries
{
    public record GetCollaboratorProductsQuery(string ListId, string Sort = "") : IRequest<Result>;
}