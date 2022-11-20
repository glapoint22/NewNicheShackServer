using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Account.ChangePassword.Commands
{
    public sealed record ChangePasswordCommand(string CurrentPassword, string NewPassword) : IRequest<Result>;
}