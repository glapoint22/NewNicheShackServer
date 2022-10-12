using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.ChangeEmail.Commands
{
    public record ChangeEmailCommand(string NewEmail, string OneTimePassword, string Password) : IRequest<Result>;
}