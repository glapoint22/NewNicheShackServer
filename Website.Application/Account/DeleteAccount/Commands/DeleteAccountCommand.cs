using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.DeleteAccount.Commands
{
    public record DeleteAccountCommand(string Token, string Password) : IRequest<Result>;
}