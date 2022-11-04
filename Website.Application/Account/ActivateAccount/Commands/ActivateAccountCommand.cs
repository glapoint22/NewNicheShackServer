using MediatR;
using Website.Application.Common.Classes;

namespace Website.Application.Account.ActivateAccount.Commands
{
    public sealed record ActivateAccountCommand(string Email, string OneTimePassword) : IRequest<Result>;
}