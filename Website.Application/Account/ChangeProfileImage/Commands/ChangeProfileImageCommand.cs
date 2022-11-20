using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Account.ChangeProfileImage.Commands
{
    public sealed record ChangeProfileImageCommand() : IRequest<Result>;
}