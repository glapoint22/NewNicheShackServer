using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.DeleteAccount.Commands
{
    public sealed record DeleteAccountCommand(string OneTimePassword, string Password) : IRequest<Result>;
}