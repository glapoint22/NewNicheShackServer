using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Lists.GetLists.Queries
{
    public sealed record GetListsQuery(string? ListId, string? Sort) : IRequest<Result>;
}