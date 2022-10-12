using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.ResetPassword.Commands
{
    public record ResetPasswordCommand(string Email, string Token, string NewPassword) : IRequest<Result>;
}
