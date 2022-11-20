using MediatR;
using Shared.Common.Classes;

namespace Website.Application.Account.DeleteAccount.Commands
{
    public sealed record DeleteAccountCommand(string OneTimePassword, string Password) : IRequest<Result>;
}