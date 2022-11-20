using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Account.ChangeEmail.Commands
{
    public sealed record ChangeEmailCommand(string NewEmail, string OneTimePassword, string Password) : IRequest<Result>;
}