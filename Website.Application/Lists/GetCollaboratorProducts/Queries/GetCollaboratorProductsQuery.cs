using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.GetCollaboratorProducts.Queries
{
    public record GetCollaboratorProductsQuery(string ListId, string? Sort = null) : IRequest<Result>;
}