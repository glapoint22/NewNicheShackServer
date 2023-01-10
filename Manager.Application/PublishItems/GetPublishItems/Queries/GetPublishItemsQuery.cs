using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.PublishItems.GetPublishItems.Queries
{
    public sealed record GetPublishItemsQuery() : IRequest<Result>;
}