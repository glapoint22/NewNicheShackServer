using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.AddNoncompliantStrikeList.Commands
{
    public sealed record AddNoncompliantStrikeListCommand(string ListId, int Option, string UserId) : IRequest<Result>;
}