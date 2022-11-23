using MediatR;
using Shared.Common.Classes;

namespace Manager.Application.Notifications.AddNoncompliantStrikeUserImage.Commands
{
    public sealed record AddNoncompliantStrikeUserImageCommand(string UserId, string UserImage) : IRequest<Result>;
}