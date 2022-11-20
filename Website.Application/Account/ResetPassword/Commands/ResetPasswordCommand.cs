using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Account.ResetPassword.Commands
{
    public sealed record ResetPasswordCommand(string Email, string OneTimePassword, string NewPassword) : IRequest<Result>;
}
