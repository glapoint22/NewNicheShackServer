using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.ActivateAccount.Commands
{
    public record ActivateAccountCommand(string Email, string Token) : IRequest<Result>;
}