using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Lists.GetSharedList.Queries
{
    public sealed record GetSharedListQuery(string ListId, string? Sort) : IRequest<Result>;
}
