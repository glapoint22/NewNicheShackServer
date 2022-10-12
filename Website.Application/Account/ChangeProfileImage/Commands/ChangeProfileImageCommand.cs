using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.ChangeProfileImage.Commands
{
    public record ChangeProfileImageCommand() : IRequest<Result>;
}