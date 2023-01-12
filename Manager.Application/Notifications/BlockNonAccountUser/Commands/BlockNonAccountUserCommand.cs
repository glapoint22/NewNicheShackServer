using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.BlockNonAccountUser.Commands
{
    public sealed record BlockNonAccountUserCommand(string Email, string UserName) : IRequest<Result>;
}