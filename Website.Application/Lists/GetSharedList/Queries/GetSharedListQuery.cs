using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.GetSharedList.Queries
{
    public record GetSharedListQuery(string ListId, string Sort) : IRequest<Result>;
}
