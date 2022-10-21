using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.ChangePassword.Commands
{
    public sealed record ChangePasswordCommand(string CurrentPassword, string NewPassword) : IRequest<Result>;
}