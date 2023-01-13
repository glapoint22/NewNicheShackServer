using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.UnblockNonAccountUser.Commands
{
    public sealed record UnblockNonAccountUserCommand(string BlockedUserEmail) : IRequest<Result>;
}