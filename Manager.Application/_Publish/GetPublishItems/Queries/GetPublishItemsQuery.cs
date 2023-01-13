using MediatR;
using Shared.Common.Classes;

namespace Manager.Application._Publish.GetPublishItems.Queries
{
    public sealed record GetPublishItemsQuery() : IRequest<Result>;
}