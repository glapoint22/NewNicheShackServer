using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.ChangeProfileImage.Commands
{
    public sealed record ChangeProfileImageCommand() : IRequest<Result>;
}