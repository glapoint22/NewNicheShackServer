using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.BlockUnblockUser.Commands
{
    public sealed record BlockUnblockUserCommand(string UserId) : IRequest<Result>;
}
